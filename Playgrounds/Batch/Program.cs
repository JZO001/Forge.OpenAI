using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Batch;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Models.Files;
using Forge.OpenAI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;

namespace Batch
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how to make a simple conversation with ChatGPT
            //
            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s)

            using (IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    services.AddForgeOpenAI(options =>
                    {
                        options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"];
                    });
                })
                .Build())
            {
                IOpenAIService openAi = host.Services.GetService<IOpenAIService>();

                // list, if we already have some completed batches
                HttpOperationResult<ListBatchesResponse> listResponse = await openAi.BatchService.GetAsync(new ListBatchesRequest());
                if (listResponse.IsSuccess)
                {
                    Console.WriteLine(listResponse.Result);

                    // find one which is completed
                    BatchData? batchData = listResponse.Result.Batches.FirstOrDefault(x => x.Status == "completed");

                    if (batchData is not null)
                    {
                        // get the file data
                        HttpOperationResult<FileDataResponse> fileDataRes = await openAi.FileService.GetFileDataAsync(batchData.OutputFileId);
                        if (fileDataRes.IsSuccess)
                        {
                            Console.WriteLine(fileDataRes.Result);

                            // download result batch file
                            using MemoryStream ms = new MemoryStream();
                            await openAi.FileService.DownloadFileAsync(batchData.OutputFileId, ms);
                            ms.Position = 0;

                            // just for demonstration, I save the file to disk
                            using FileStream fs = new FileStream(fileDataRes.Result.FileName, FileMode.Create, FileAccess.Write);
                            await ms.CopyToAsync(fs);
                            ms.Position = 0;

                            // process the jsonl content with JsonlManager, I expect ChatCompletionResponse
                            JsonlManager<BatchRequestOutput<ChatCompletionResponse>> jsonlManager = JsonlManager<BatchRequestOutput<ChatCompletionResponse>>.Load(ms);
                            jsonlManager.Items.ToList().ForEach(x =>
                            {
                                x.Response.Body.Choices.ToList().ForEach(y =>
                                {
                                    Console.WriteLine(y.Message.Content);
                                });
                            });

                        }
                        else
                        {
                            Console.WriteLine("Failed");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Failed");
                }

                string fileId = await PrepareBatchRequestAsync(openAi);

                CreateBatchRequest request = new CreateBatchRequest(fileId, "/v1/chat/completions");
                request.Metadata = new Dictionary<string, string>
                {
                    { "description", "nightly eval job" }
                };

                HttpOperationResult<CreateBatchResponse> response = openAi.BatchService.CreateAsync(request, CancellationToken.None).Result;
                if (response.IsSuccess)
                {
                    string batchId = response.Result.Id;

                    HttpOperationResult<BatchResponse> statusQueryResult = await openAi.BatchService.GetAsync(batchId);
                    if (statusQueryResult.IsSuccess)
                    {
                        Console.WriteLine(statusQueryResult.Result);
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }

                    HttpOperationResult<CancelBatchResponse> cancelBatchResponse = await openAi.BatchService.CancelAsync(batchId);
                    if (cancelBatchResponse.IsSuccess)
                    {
                        Console.WriteLine(cancelBatchResponse.Result);
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }

                }
                else
                {
                    Console.WriteLine("Failed");
                }

            }
        }

        static async Task<string> PrepareBatchRequestAsync(IOpenAIService openAi)
        {
            // https://platform.openai.com/docs/guides/batch/getting-started

            JsonlManager<BatchRequestInput> jsonpManager = new JsonlManager<BatchRequestInput>();
            jsonpManager.Add(new BatchRequestInput
            {
                CustomId = "request-1",
                Method = BatchRequestInput.METHOD_POST,
                Url = "/v1/chat/completions",
                Body = new ChatCompletionRequest(ChatMessage.CreateFromUser("Count to 20, with a comma between each number and no newlines. E.g., 1, 2, 3, ..."))
            });

            jsonpManager.Add(new BatchRequestInput
            {
                CustomId = "request-2",
                Method = BatchRequestInput.METHOD_POST,
                Url = "/v1/chat/completions",
                Body = new ChatCompletionRequest(ChatMessage.CreateFromUser("Count to 50, with a comma between each number and no newlines. E.g., 1, 2, 3, ..."))
            });

            // upload batch file

            using MemoryStream ms = new MemoryStream();
            jsonpManager.Save(ms);
            ms.Position = 0;

            FileUploadRequest uploadRequest = new FileUploadRequest();
            uploadRequest.File = new BinaryContentData() { ContentName = "batch_demo.jsonl", SourceStream = ms };
            uploadRequest.Purpose = FileUploadRequest.PURPOSE_BATCH;

            HttpOperationResult<FileUploadResponse> responseUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None);
            if (responseUpload.IsSuccess)
            {
                return responseUpload.Result.Id; // id of the uploaded file
            }

            return string.Empty;
        }

    }

}
