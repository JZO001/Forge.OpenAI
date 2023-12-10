using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using Forge.OpenAI.Models.FineTuningJob;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FineTuningJob
{

    /// <summary>
    /// Resources:
    /// https://platform.openai.com/docs/guides/fine-tuning/preparing-your-dataset
    /// https://platform.openai.com/docs/guides/prompt-engineering/six-strategies-for-getting-better-results
    /// </summary>
    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can fine tune a model with the information you provide.
            // More information: https://platform.openai.com/docs/api-reference/fine-tuning
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

            Console.WriteLine("Uploading training file...");

            FileUploadRequest uploadRequest = new FileUploadRequest();
            uploadRequest.File = new BinaryContentData() { ContentName = "training", SourceStream = File.OpenRead("training.jsonl") };
            uploadRequest.SetPurposeAsEnum = PurposeTypeEnum.FineTune;

            HttpOperationResult<FileUploadResponse> responseUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None);
            if (responseUpload.IsSuccess)
            {
                Console.WriteLine($"Uploaded, id: {responseUpload.Result!.Id}");
                Console.WriteLine("Creating fine tuning job");
                Console.WriteLine();

                FineTuningJobCreateRequest createRequest = new FineTuningJobCreateRequest();
                createRequest.TrainingFileId = responseUpload.Result!.Id;

                HttpOperationResult<FineTuningJobResponse> createResponse = await openAi.FineTuningJobService.CreateAsync(createRequest, CancellationToken.None);
                if (createResponse.IsSuccess)
                {
                    Console.WriteLine($"Job created, id: {createResponse.Result!.Id}");
                    Console.WriteLine("List fine tuning jobs");
                    Console.WriteLine();

                    HttpOperationResult<FineTuningJobListResponse> listResponse = await openAi.FineTuningJobService.GetAsync(null, CancellationToken.None);
                    if (listResponse.IsSuccess)
                    {
                        listResponse.Result!.Data.ToList().ForEach(job =>
                        {
                            Console.WriteLine(job);
                            Console.WriteLine();
                        });
                    }
                    else
                    {
                        Console.WriteLine(listResponse);
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Retrieve fine tuning job data, id: {createResponse.Result!.Id}");
                    Console.WriteLine();

                    HttpOperationResult<FineTuningJobResponse> responseJobData = await openAi.FineTuningJobService.GetAsync(createResponse.Result!.Id, CancellationToken.None);
                    if (responseJobData.IsSuccess)
                    {
                        Console.WriteLine(responseJobData.Result!);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(responseJobData);
                    }

                    Console.WriteLine();
                    Console.WriteLine("List fine tuning events (sync mode)");
                    Console.WriteLine();

                    HttpOperationResult<FineTuningJobEvent> eventsResponse = await openAi.FineTuningJobService.GetEventsAsync(createResponse.Result!.Id, CancellationToken.None);
                    if (eventsResponse.IsSuccess)
                    {
                        Console.WriteLine(eventsResponse.Result!);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(eventsResponse);
                    }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(async () => {
                        Console.WriteLine();
                        Console.WriteLine("List fine tuning events (async mode - callback)");
                        Console.WriteLine();

                        Action<HttpOperationResult<FineTuningJobEvent>> eventResultCallback = (HttpOperationResult<FineTuningJobEvent> response) =>
                        {
                            if (response.IsSuccess)
                            {
                                Console.WriteLine("RESULT (async mode - callback)");
                                Console.WriteLine(response.Result!);
                            }
                            else
                            {
                                Console.WriteLine(response);
                            }
                        };

                        HttpOperationResult eventsCallbackModeResponse = await openAi.FineTuningJobService.GetEventsAsStreamAsync(createResponse.Result!.Id, eventResultCallback, CancellationToken.None);
                        if (eventsCallbackModeResponse.IsSuccess)
                        {
                            Console.WriteLine();
                            Console.WriteLine("DONE (async mode - callback)");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine(eventsCallbackModeResponse);
                        }
                    });

                    Task.Run(async () => {
                        Console.WriteLine();
                        Console.WriteLine("List fine tuning job events (async mode - IAsyncEnumerable)");
                        Console.WriteLine();

                        await foreach (HttpOperationResult<FineTuningJobEvent> response in openAi.FineTuningJobService.GetEventsAsStreamAsync(createResponse.Result!.Id, CancellationToken.None))
                        {
                            if (response.IsSuccess)
                            {
                                Console.WriteLine("RESULT (async mode - IAsyncEnumerable)");
                                Console.WriteLine(response.Result!);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(response);
                            }
                        }
                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                    Console.WriteLine("Press a key to cancel fine tuning job and release async event readers");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine("Cancel fine tuning job");
                    Console.WriteLine();

                    HttpOperationResult<FineTuningJobResponse> responseCancel = await openAi.FineTuningJobService.CancelAsync(createResponse.Result!.Id, CancellationToken.None);
                    if (responseCancel.IsSuccess)
                    {
                        Console.WriteLine(responseCancel.Result!);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(responseCancel);
                    }

                }
                else
                {
                    Console.WriteLine(createResponse);
                }

                Console.WriteLine();
                Console.WriteLine("Deleting training file");
                await openAi.FileService.DeleteFileAsync(responseUpload.Result!.Id, CancellationToken.None);

            }
            else
            {
                Console.WriteLine(responseUpload);
            }
        }

    }
}
