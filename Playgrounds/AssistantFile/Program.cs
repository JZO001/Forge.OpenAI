using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using Forge.OpenAI.Models.Files;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Forge.OpenAI.Models.Shared;

namespace AssistantFile
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/file-object
    /// https://platform.openai.com/docs/assistants/tools/uploading-files-for-retrieval
    /// https://platform.openai.com/docs/assistants/tools/supported-files
    /// </summary>
    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can you upload and administrate a file for an assistant.
            // More information: https://platform.openai.com/docs/api-reference/assistants/file-object
            //
            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s)

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

            // demo: first upload a training file, a knowledge file, which the assistant can use to answer questions
            FileUploadRequest uploadRequest = new FileUploadRequest();
            uploadRequest.File = new BinaryContentData() { ContentName = "training", SourceStream = File.OpenRead("LearningGuideWindows10.docx") };
            uploadRequest.Purpose = FileUploadRequest.PURPOSE_ASSISTANTS;

            HttpOperationResult<FileUploadResponse> responseFileUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None);
            if (responseFileUpload.IsSuccess)
            {
                // Create an assistant
                CreateAssistantRequest request = new CreateAssistantRequest()
                {
                    Model = KnownModelTypes.Gpt3_5Turbo_1106,
                    Name = "Windows 10 Tutor",
                    Instructions = "You are a personal windows 10 tutor. When asked a question, explain and answer the question.",
                    Tools = new List<Tool>()
                    {
                        new Tool() { Type = Tool.RETRIEVAL }
                    }
                };

                HttpOperationResult<AssistantResponse> createAssistantResult = await openAi.AssistantService.CreateAsync(request, CancellationToken.None);
                if (createAssistantResult.IsSuccess)
                {
                    // demo: create an assistant file
                    CreateAssistantFileRequest assistantFileUploadRequest = new CreateAssistantFileRequest()
                    {
                        AssistantId = createAssistantResult.Result!.Id,
                        FileId = responseFileUpload.Result!.Id
                    };
                    HttpOperationResult<AssistantFileResponse> createFileResult = await openAi.AssistantFileService.CreateAsync(assistantFileUploadRequest, CancellationToken.None);
                    if (createFileResult.IsSuccess)
                    {
                        Console.WriteLine(createFileResult.Result!);
                        Console.WriteLine();
                    }

                    // demo: retrieve the assistant file
                    HttpOperationResult<AssistantFileResponse> getFileResult = await openAi.AssistantFileService.GetAsync(createAssistantResult.Result!.Id, responseFileUpload.Result!.Id, CancellationToken.None);
                    if (getFileResult.IsSuccess)
                    {
                        Console.WriteLine(getFileResult.Result!);
                        Console.WriteLine();
                    }

                    // demo: retrieve the assistant file list
                    AssistantFileListRequest assistantFileListRequest = new AssistantFileListRequest() 
                    { 
                        AssistantId = createAssistantResult.Result!.Id
                        // you can also implement paging operations with the request
                    };
                    HttpOperationResult<AssistantFileListResponse> getFileListResult = await openAi.AssistantFileService.GetAsync(assistantFileListRequest, CancellationToken.None);
                    if (getFileListResult.IsSuccess)
                    {
                        getFileListResult.Result!.Data.ToList().ForEach(file =>
                        {
                            Console.WriteLine(file);
                            Console.WriteLine();
                        });
                    }

                    // demo: delete the assistant file
                    HttpOperationResult<DeleteStateResponse> deleteFileResult = await openAi.AssistantFileService.DeleteAsync(createAssistantResult.Result!.Id, createFileResult.Result!.Id, CancellationToken.None);
                    if (deleteFileResult.IsSuccess)
                    {
                        Console.WriteLine(deleteFileResult.Result!);
                    }

                    // demo: at the end of the demo, I just remove the assistant too
                    HttpOperationResult<DeleteStateResponse> deleteAssistantResult = await openAi.AssistantService.DeleteAsync(createAssistantResult.Result!.Id, CancellationToken.None);
                    if (deleteAssistantResult.IsSuccess)
                    {
                        Console.WriteLine(deleteAssistantResult.Result!);
                    }
                }

                // demo: at the end of the demo, I just remove uploaded file also
                HttpOperationResult<FileDeleteResponse> deleteResult = await openAi.FileService.DeleteFileAsync(responseFileUpload.Result!.Id, CancellationToken.None);
                if (deleteResult.IsSuccess)
                {
                    Console.WriteLine(deleteResult.Result!);
                }
            }

        }

    }

}
