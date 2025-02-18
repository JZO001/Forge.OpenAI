using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Assistants;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using Forge.OpenAI.Models.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using Forge.OpenAI.Models.Runs;
using Forge.OpenAI.Models.Threads;
using Forge.OpenAI.Models.VectorStores;
using Forge.OpenAI.Models.VectorStoreFileBatches;
using Forge.OpenAI.ErrorOr;

namespace ErrorOr.Concept;

internal class Program
{

    private const string SENTENCES_FILE_NAME = "sentences.txt";
    private const string INSTRUCTION_FILE_NAME = "instruction.txt";
    private const string ASSISTANT_NAME = "Abbreviation Assistant";
    private const string VECTOR_STORE_NAME = "Abbreviation Vector Store";
    private const string OUTPUT_JSON_FILE_NAME = "output.json";

    private const string ASSISTANT_MODEL = KnownModelTypes.Gpt_4o;
    private const double TEMPERATURE = 0.2;
    private const double TOP_P = 1.0;

    private static bool ReUploadFiles { get; set; } = false;
    private static bool ReInitializeAssistant { get; set; } = false;

    private static Dictionary<string, string> FileNamesWithIds = new()
    {
        { "abbreviations.txt", "" }
    };

    private static string VectorStoreId { get; set; } = string.Empty;

    private static string AssistantId { get; set; } = string.Empty;

    private static string Sentences { get; set; } = string.Empty;

    private static string Instructions { get; set; } = string.Empty;

    static async Task Main(string[] args)
    {
        ReUploadFiles = args.Contains("--reupload");
        ReInitializeAssistant = args.Contains("--reinit");

        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddForgeOpenAI(options =>
                {
                    options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
                });
            })
            .Build();

        IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;
        
        CancellationToken cancellationToken = CancellationToken.None;

        await (await ReadSentencesFileAsync(SENTENCES_FILE_NAME, cancellationToken))
            .ThenAsync(async _ => await ReadInstructionFileAsync(INSTRUCTION_FILE_NAME, cancellationToken))
            .ThenAsync(async _ => await CheckFilesAndQueryIds(openAi, cancellationToken))
            .ThenAsync(async _ => await CheckOrInitVectorStoreAsync(openAi, cancellationToken))
            .ThenAsync(async _ => await CheckOrInitAssistantAsync(openAi, cancellationToken))
            .ThenAsync(async _ => await QueryAssistantAsync(openAi, AssistantId, Sentences, cancellationToken))
            .Else(error =>
            {
                Environment.Exit(1);
                Console.WriteLine(error[0].Description);
                return error[0];
            });
    }

    #region Workflow

    static async Task<ErrorOr<Success>> ReadSentencesFileAsync(string fileName, CancellationToken cancellationToken)
    {
        if (!File.Exists(fileName)) return Error.Failure($"File '{fileName}' not found.");

        Sentences = await File.ReadAllTextAsync(fileName, cancellationToken);

        return Result.Success;
    }

    static async Task<ErrorOr<Success>> ReadInstructionFileAsync(string fileName, CancellationToken cancellationToken)
    {
        if (!File.Exists(fileName)) return Error.Failure($"File '{fileName}' not found.");

        Instructions = await File.ReadAllTextAsync(fileName, cancellationToken);

        Instructions = string.Format(Instructions, GetOutputSchema(), GetOutputExample());

        return Result.Success;
    }

    static async Task<ErrorOr<Success>> CheckFilesAndQueryIds(IOpenAIService openAi, CancellationToken cancellationToken)
    {
        foreach (string fileName in FileNamesWithIds.Keys.ToList())
        {
            ErrorOr<IFileData> fileDataResult = await IsFileExistsAsync(openAi, fileName, cancellationToken);

            if (fileDataResult.IsError)
            {
                if (fileDataResult.FirstError.Code == Error.NotFound().Code)
                {
                    Console.WriteLine($"File '{fileName}' not found. Uploading file...");

                    fileDataResult = await UploadFileAsync(openAi, new FileInfo(fileName), cancellationToken);
                    if (fileDataResult.IsError)
                    {
                        return Error.Failure($"Failed to upload file '{fileName}'. Error: {fileDataResult.FirstError.Description}");
                    }

                    FileNamesWithIds[fileName] = fileDataResult.Value.Id;
                }
                else
                {
                    return Error.Failure($"Failed to query file '{fileName}'. Error: {fileDataResult.FirstError.Description}");
                }
            }
            else if (ReUploadFiles)
            {
                HttpOperationResult<FileDeleteResponse> delResult = await openAi
                    .FileService
                    .DeleteFileAsync(fileDataResult.Value.Id, cancellationToken);

                if (delResult.IsSuccess)
                {
                    Console.WriteLine($"File '{fileName}' deleted. Uploading file...");

                    fileDataResult = await UploadFileAsync(openAi, new FileInfo(fileName), cancellationToken);
                    if (fileDataResult.IsError)
                    {
                        return Error.Failure($"Failed to upload file '{fileName}'. Error: {fileDataResult.FirstError.Description}");
                    }

                    FileNamesWithIds[fileName] = fileDataResult.Value.Id;
                }
                else
                {
                    return Error.Failure($"Failed to delete file '{fileName}'. Error: {delResult.ErrorMessage}");
                }
            }
            else
            {
                FileNamesWithIds[fileName] = fileDataResult.Value.Id;
            }
        }

        return Result.Success;
    }

    static async Task<ErrorOr<Success>> CheckOrInitVectorStoreAsync(IOpenAIService openAi, CancellationToken cancellationToken)
    {
        ErrorOr<IVectorStoreData> vectorStoreDataResult = await IsVectorStoreExistAsync(openAi, VECTOR_STORE_NAME, cancellationToken);

        if (vectorStoreDataResult.IsError)
        {
            if (vectorStoreDataResult.FirstError.Code == Error.NotFound().Code)
            {
                Console.WriteLine($"Vector Store '{VECTOR_STORE_NAME}' not found. Creating...");

                vectorStoreDataResult = await CreateVectorStoreAsync(openAi, VECTOR_STORE_NAME, FileNamesWithIds.Values, cancellationToken);
                if (vectorStoreDataResult.IsError)
                {
                    return Error.Failure(description: $"Failed to create Vector Store '{VECTOR_STORE_NAME}'. Error: {vectorStoreDataResult.FirstError.Description}", metadata: vectorStoreDataResult.FirstError.Metadata);
                }

                VectorStoreId = vectorStoreDataResult.Value.Id;
            }
            else
            {
                return Error.Failure(description: $"Failed to query Vector Store '{VECTOR_STORE_NAME}'. Error: {vectorStoreDataResult.FirstError.Description}", metadata: vectorStoreDataResult.FirstError.Metadata);
            }
        }
        else if (ReInitializeAssistant)
        {
            Console.WriteLine($"Modifying Vector Store '{VECTOR_STORE_NAME}'...");

            ErrorOr<IVectorStoreFileBatchData> vectorBatchData = await ModifyVectorStoreAsync(openAi, vectorStoreDataResult.Value.Id, FileNamesWithIds.Values, cancellationToken);
            if (vectorBatchData.IsError)
            {
                return Error.Failure(description: $"Failed to modify Vector Store '{VECTOR_STORE_NAME}'. Error: {vectorBatchData.FirstError.Description}", metadata: vectorBatchData.FirstError.Metadata);
            }

            VectorStoreId = vectorBatchData.Value.VectorStoreId;
        }
        else
        {
            VectorStoreId = vectorStoreDataResult.Value.Id;
        }

        return Result.Success;
    }

    static async Task<ErrorOr<Success>> CheckOrInitAssistantAsync(IOpenAIService openAi, CancellationToken cancellationToken)
    {
        ErrorOr<IAssistantData> assistantDataResult = await IsAssistantExistAsync(openAi, ASSISTANT_NAME, cancellationToken);

        if (assistantDataResult.IsError)
        {
            if (assistantDataResult.FirstError.Code == Error.NotFound().Code)
            {
                Console.WriteLine($"Assistant '{ASSISTANT_NAME}' not found. Creating...");

                assistantDataResult = await CreateAssistantAsync(openAi, ASSISTANT_NAME, Instructions, VectorStoreId, cancellationToken);
                if (assistantDataResult.IsError)
                {
                    return Error.Failure(description: $"Failed to create assistant '{ASSISTANT_NAME}'. Error: {assistantDataResult.FirstError.Description}", metadata: assistantDataResult.FirstError.Metadata);
                }

                AssistantId = assistantDataResult.Value.Id;
            }
            else
            {
                return Error.Failure(description: $"Failed to query assistant '{ASSISTANT_NAME}'. Error: {assistantDataResult.FirstError.Description}", metadata: assistantDataResult.FirstError.Metadata);
            }
        }
        else if (ReInitializeAssistant)
        {
            Console.WriteLine($"Modifying assistant '{ASSISTANT_NAME}'...");

            assistantDataResult = await ModifyAssistantAsync(openAi, assistantDataResult.Value.Id, ASSISTANT_NAME, Instructions, VectorStoreId, cancellationToken);
            if (assistantDataResult.IsError)
            {
                return Error.Failure(description: $"Failed to create assistant '{ASSISTANT_NAME}'. Error: {assistantDataResult.FirstError.Description}", metadata: assistantDataResult.FirstError.Metadata);
            }
        }

        AssistantId = assistantDataResult.Value.Id;

        return Result.Success;
    }

    #endregion

    #region OpenAI Operations

    static async Task<ErrorOr<IFileData>> IsFileExistsAsync(IOpenAIService openAi, string contentName, CancellationToken cancellationToken)
    {
        return (await openAi.FileService.GetFileListAsync(cancellationToken))
            .AsErrorOr()
            .Then<IFileData>(response => 
            {
                FileData? fileData = response
                    .Files
                    .FirstOrDefault(f => string.Equals(f.Purpose, FileUploadRequest.PURPOSE_ASSISTANTS, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(contentName, f.FileName, StringComparison.OrdinalIgnoreCase));

                if (fileData is null) return Error.NotFound();

                return fileData;
            })
            .Else(errors => errors[0]);
    }

    static async Task<ErrorOr<IFileData>> UploadFileAsync(IOpenAIService openAi, FileInfo fileInfo, CancellationToken cancellationToken)
    {
        FileUploadRequest uploadRequest = new FileUploadRequest();
        uploadRequest.File = new BinaryContentData() { ContentName = fileInfo.Name, SourceStream = File.OpenRead(fileInfo.FullName) };
        uploadRequest.Purpose = FileUploadRequest.PURPOSE_ASSISTANTS;

        return (await openAi
                    .FileService
                    .UploadFileAsync(uploadRequest, cancellationToken)
                )
                .AsErrorOr<FileUploadResponse, IFileData>();
    }

    static async Task<ErrorOr<IVectorStoreData>> IsVectorStoreExistAsync(IOpenAIService openAi, string vectorStoreName, CancellationToken cancellationToken)
    {
        VectorStoreListRequest listQueryRequest = new VectorStoreListRequest();

        return (await openAi.VectorStoreService.GetAsync(listQueryRequest, cancellationToken))
            .AsErrorOr()
            .Then<IVectorStoreData>(response =>
            {
                VectorStoreData? vectorStoreData = response
                    .Data
                    .FirstOrDefault(v => string.Equals(vectorStoreName, v.Name, StringComparison.OrdinalIgnoreCase));

                if (vectorStoreData is null) return Error.NotFound();

                return vectorStoreData;
            })
            .Else(errors => errors[0]);
    }

    static async Task<ErrorOr<IVectorStoreData>> CreateVectorStoreAsync(IOpenAIService openAi, string vectorStoreName, IEnumerable<string> fileIds, CancellationToken cancellationToken)
    {
        CreateVectorStoreRequest createRequest = new CreateVectorStoreRequest()
        {
            Name = vectorStoreName,
            FileIds = fileIds.ToList().AsReadOnly()
        };

        return (await openAi
                    .VectorStoreService
                    .CreateAsync(createRequest, cancellationToken)
                )
                .AsErrorOr<CreateVectorStoreResponse, IVectorStoreData>();
    }

    static async Task<ErrorOr<IVectorStoreFileBatchData>> ModifyVectorStoreAsync(
        IOpenAIService openAi,
        string vectorStoreId,
        IEnumerable<string> fileIds,
        CancellationToken cancellationToken)
    {
        CreateVectorStoreFileBatchRequest updateRequest = new CreateVectorStoreFileBatchRequest(vectorStoreId)
        {
            FileIds = fileIds.ToList()
        };

        return (await openAi
                    .VectorStoreFileBatchService
                    .CreateAsync(updateRequest, cancellationToken)
                )
                .AsErrorOr<CreateVectorStoreFileBatchResponse, IVectorStoreFileBatchData>();
    }

    static async Task<ErrorOr<IAssistantData>> IsAssistantExistAsync(IOpenAIService openAi, string assistantName, CancellationToken cancellationToken)
    {
        AssistantListRequest listQueryRequest = new AssistantListRequest();

        return (await openAi.AssistantService.GetAsync(listQueryRequest, cancellationToken))
            .AsErrorOr()
            .Then<IAssistantData>(response =>
            {
                AssistantData? assistantData = response
                    .Data
                    .FirstOrDefault(a => string.Equals(assistantName, a.Name, StringComparison.OrdinalIgnoreCase));

                if (assistantData is null) return Error.NotFound();

                return assistantData;
            })
            .Else(errors => errors[0]);
    }

    static async Task<ErrorOr<IAssistantData>> CreateAssistantAsync(IOpenAIService openAi, 
        string assistantName, 
        string instructions,
        string vectorStoreId,
        CancellationToken cancellationToken)
    {
        CreateAssistantRequest createRequest = new CreateAssistantRequest()
        {
            Model = ASSISTANT_MODEL,
            Name = assistantName,
            Instructions = instructions,
            Temperature = TEMPERATURE,
            TopP = TOP_P,
            ResponseFormatAsObject = new ResponseFormat() { Type = ResponseFormat.RESPONSE_FORMAT_JSON },
            //Tools = new List<Tool>()
            //{
            //    new Tool() { Type = Tool.CODE_INTERPRETER }
            //},
            ToolResources = new ToolResource()
            {
                FileSearch = new FileSearch()
                {
                    VectorStoreIds = new List<string>() { vectorStoreId }
                }
            }
        };

        return (await openAi
                    .AssistantService
                    .CreateAsync(createRequest, cancellationToken)
                )
                .AsErrorOr<AssistantResponse, IAssistantData>();
    }

    static async Task<ErrorOr<IAssistantData>> ModifyAssistantAsync(
        IOpenAIService openAi,
        string assistantId,
        string assistantName,
        string instructions,
        string vectorStoreId,
        CancellationToken cancellationToken)
    {
        ModifyAssistantRequest updateRequest = new ModifyAssistantRequest()
        {
            AssistantId = assistantId,
            Model = ASSISTANT_MODEL,
            Name = assistantName,
            Instructions = instructions,
            Temperature = TEMPERATURE,
            TopP = TOP_P,
            ResponseFormatAsObject = new ResponseFormat() { Type = ResponseFormat.RESPONSE_FORMAT_JSON },
            //Tools = new List<Tool>()
            //{
            //    new Tool() { Type = Tool.CODE_INTERPRETER }
            //},
            ToolResources = new ToolResource()
            {
                FileSearch = new FileSearch()
                {
                    VectorStoreIds = new List<string>() { vectorStoreId }
                }
            }
        };

        return (await openAi
                    .AssistantService
                    .ModifyAsync(updateRequest, cancellationToken)
                )
                .AsErrorOr<AssistantResponse, IAssistantData>();
    }

    static async Task<ErrorOr<Success>> QueryAssistantAsync(IOpenAIService openAi, string assistantId, string sentences, CancellationToken cancellationToken)
    {
        CreateThreadAndRunRequest createThreadAndRunRequest = new()
        {
            AssistantId = assistantId,
            Thread = new CreateThreadRequest()
            {
                Messages = new List<Message>()
                {
                    new Message(sentences)
                    {
                        Role = Message.ROLE_USER
                    }
                }
            },
            Stream = true
        };

        string threadId = string.Empty;

        ErrorOr<Success> streamResult = (await openAi.RunService.CreateThreadAndRunAsStreamAsync(createThreadAndRunRequest, (run) =>
        {
            if (run.IsSuccess)
            {
                CreateThreadAndRunResponse response = run.Result!.Data;
                threadId = response.ThreadId;

                if (response.Status == RunResponseBase.RUN_STATUS_COMPLETED && response.Object == "thread.message")
                {
                    foreach (MessageDeltaContent messageDeltaContent in response.MessageDeltaContents)
                    {
                        if (messageDeltaContent.Text is not null)
                        {
                            using FileStream fileStream = new FileStream(OUTPUT_JSON_FILE_NAME, FileMode.Create, FileAccess.Write, FileShare.Read);
                            using StreamWriter writer = new StreamWriter(fileStream);
                            writer.Write(messageDeltaContent.Text.Value);
                            writer.Flush();
                            break;
                        }
                    }
                }
            }
        }, cancellationToken)).AsErrorOr();

        await openAi.ThreadsService.DeleteAsync(threadId, cancellationToken);

        if (streamResult.IsError)
        {
            return streamResult;
        }

        if (!File.Exists(OUTPUT_JSON_FILE_NAME))
        {
            return Error.Failure(description: "Output file was not created.");
        }

        return Result.Success;
    }

    #endregion

    #region Other method(s)

    static string GetOutputSchema()
    {
        JsonSchemaGenerator schemaGenerator = new();
        Newtonsoft.Json.Schema.JsonSchema schema = schemaGenerator.Generate(typeof(AbbreviationsOutput));
        return schema.ToString();
    }

    static string GetOutputExample()
    {
        AbbreviationsOutput output = new AbbreviationsOutput();

        output.Abbreviations.Add(new Abbreviation()
        {
            OriginalSentence = "This is a test sentence.",
            AbbreviationList = new List<string>()
            {
                "This is a tst sent.",
                "This's a tst sent.",
                "This = tst sent."
            }
        });

        output.Abbreviations.Add(new Abbreviation()
        {
            OriginalSentence = "Long sentence should be abbreviated.",
            AbbreviationList = new List<string>()
            {
                "Lng sent. shld be abbr.",
                "Lng sent. shld b abbr.",
                "Lng sent. → abbr."
            }
        });

        return JsonConvert.SerializeObject(output, Formatting.Indented);
    }

    #endregion

}
