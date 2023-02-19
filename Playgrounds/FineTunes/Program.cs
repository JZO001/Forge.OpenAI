using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using Forge.OpenAI.Models.FineTunes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FineTunes
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can fine tune a model with the information you provide.
            // More information: https://platform.openai.com/docs/guides/fine-tuning
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
            uploadRequest.Purpose = "fine-tune";

            HttpOperationResult<FileUploadResponse> responseUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None).ConfigureAwait(false);
            if (responseUpload.IsSuccess)
            {
                Console.WriteLine($"Uploaded, id: {responseUpload.Result!.Id}");
                Console.WriteLine("Creating fine tune job");
                Console.WriteLine();

                FineTuneCreateRequest createRequest = new FineTuneCreateRequest();
                createRequest.TrainingFileId = responseUpload.Result!.Id;

                HttpOperationResult<FineTuneCreateResponse> createResponse = await openAi.FineTuneService.CreateAsync(createRequest, CancellationToken.None).ConfigureAwait(false);
                if (createResponse.IsSuccess)
                {
                    Console.WriteLine($"Job created, id: {createResponse.Result!.Id}");
                    Console.WriteLine("List fine tune jobs");
                    Console.WriteLine();

                    HttpOperationResult<FineTuneListResponse> listResponse = await openAi.FineTuneService.GetAsync(CancellationToken.None).ConfigureAwait(false);
                    if (listResponse.IsSuccess)
                    {
                        listResponse.Result!.Jobs.ForEach(job =>
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
                    Console.WriteLine($"Retrieve fine tune job data, id: {createResponse.Result!.Id}");
                    Console.WriteLine();

                    HttpOperationResult<FineTuneJobDataResponse> responseJobData = await openAi.FineTuneService.GetAsync(createResponse.Result!.Id, CancellationToken.None).ConfigureAwait(false);
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
                    Console.WriteLine("List fine tune events (sync mode)");
                    Console.WriteLine();

                    HttpOperationResult<FineTuneJobEventsResponse> eventsResponse = await openAi.FineTuneService.GetEventsAsync(createResponse.Result!.Id, CancellationToken.None).ConfigureAwait(false);
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
                        Console.WriteLine("List fine tune events (async mode - callback)");
                        Console.WriteLine();

                        Action<HttpOperationResult<FineTuneJobEvent>> eventResultCallback = (HttpOperationResult<FineTuneJobEvent> response) =>
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

                        HttpOperationResult eventsCallbackModeResponse = await openAi.FineTuneService.GetEventsAsStreamAsync(createResponse.Result!.Id, eventResultCallback, CancellationToken.None).ConfigureAwait(false);
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
                        Console.WriteLine("List fine tune events (async mode - IAsyncEnumerable)");
                        Console.WriteLine();

                        await foreach (HttpOperationResult<FineTuneJobEvent> response in openAi.FineTuneService.GetEventsAsStreamAsync(createResponse.Result!.Id, CancellationToken.None))
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

                    Console.WriteLine("Press a key to cancel fine tune job and release async event readers");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine("Cancel fine tune job");
                    Console.WriteLine();

                    HttpOperationResult<FineTuneCancelResponse> responseCancel = await openAi.FineTuneService.CancelAsync(createResponse.Result!.Id, CancellationToken.None).ConfigureAwait(false);
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