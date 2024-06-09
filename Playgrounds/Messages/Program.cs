using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Threads;
using Forge.OpenAI.Models.Messages;
using Forge.OpenAI.Models.Files;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Messages
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can you create, configure and administrate a thread.
            // More information: https://platform.openai.com/docs/api-reference/messages
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

            Console.WriteLine("Creating thread");
            Console.WriteLine();

            // Create an thread
            CreateThreadRequest threadCreateRequest = new CreateThreadRequest();

            HttpOperationResult<CreateThreadResponse> threadCreateResult = await openAi.ThreadsService.CreateAsync(threadCreateRequest, CancellationToken.None);
            if (threadCreateResult.IsSuccess)
            {
                Console.WriteLine(threadCreateResult.Result!);
                Console.WriteLine();

                string threadId = threadCreateResult.Result!.Id;

                // test a text based message
                await Test_TextMessageAsync(openAi, threadId);

                await Test_FileMessageAsync(openAi, threadId);

                // remove thread from demo environment
                HttpOperationResult<DeleteThreadResponse> deleteResult = await openAi.ThreadsService.DeleteAsync(threadId, CancellationToken.None);
                if (deleteResult.IsSuccess)
                {
                    Console.WriteLine($"Deleted: {deleteResult.Result!.Deleted}");
                }

            }
            else
            {
                Console.WriteLine("Unable to create thread");
                Console.WriteLine(threadCreateResult);
            }

        }

        private static async Task Test_TextMessageAsync(IOpenAIService openAi, string threadId)
        {
            Console.WriteLine("Creating message");
            Console.WriteLine();

            // Create a message
            CreateMessageRequest createMessageRequest = new CreateMessageRequest()
            {
                ThreadId = threadId,
                ContentAsString = "Hello, how are you?"
            };

            HttpOperationResult<CreateMessageResponse> createMessageResult = await openAi.MessagesService.CreateAsync(createMessageRequest, CancellationToken.None);
            if (createMessageResult.IsSuccess)
            {
                Console.WriteLine(createMessageResult.Result!);
                Console.WriteLine();

                string messageId = createMessageResult.Result!.Id;

                // demo: how to query the message by id
                HttpOperationResult<MessageResponse> queryResult = await openAi.MessagesService.GetAsync(threadId, messageId, CancellationToken.None);
                if (queryResult.IsSuccess)
                {
                    Console.WriteLine(queryResult.Result!);
                    Console.WriteLine();
                }

                // demo: how to query the list of messages
                MessageListRequest listRequest = new MessageListRequest()
                {
                    ThreadId = threadId
                };
                HttpOperationResult<MessageListResponse> queryListResult = await openAi.MessagesService.GetAsync(listRequest, CancellationToken.None);
                if (queryListResult.IsSuccess)
                {
                    Console.WriteLine(queryListResult.Result!.HasMore);
                    Console.WriteLine(queryListResult.Result!.FirstId);
                    Console.WriteLine(queryListResult.Result!.LastId);
                    foreach (MessageData messageData in queryListResult.Result!.Data)
                    {
                        Console.WriteLine(messageData.Id);
                    }
                    Console.WriteLine();
                }

                // demo: change the parameters of a message
                ModifyMessageRequest modifyRequest = new ModifyMessageRequest()
                {
                    ThreadId = threadId,
                    MessageId = messageId,
                    Metadata = new Dictionary<string, string>()
                        {
                            { "modified", "true" },
                            { "user", "abc123" }
                        }
                };
                HttpOperationResult<ModifyMessageResponse> modifyResult = await openAi.MessagesService.ModifyAsync(modifyRequest, CancellationToken.None);
                if (modifyResult.IsSuccess)
                {
                    Console.WriteLine(modifyResult.Result!);
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("Unable to create message");
                Console.WriteLine(createMessageResult);
            }
        }

        private static async Task Test_FileMessageAsync(IOpenAIService openAi, string threadId)
        {
            FileUploadRequest uploadRequest = new FileUploadRequest();
            uploadRequest.File = new BinaryContentData() { ContentName = "training", SourceStream = File.OpenRead("training.jsonl") };
            uploadRequest.Purpose = FileUploadRequest.PURPOSE_ASSISTANTS;

            HttpOperationResult<FileUploadResponse> responseUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None);
            if (responseUpload.IsSuccess)
            {
                Console.WriteLine(responseUpload.Result!);
                Console.WriteLine();

                // Create a message
                CreateMessageRequest createMessageRequest = new CreateMessageRequest()
                {
                    ThreadId = threadId,
                    Content = "What kind of product company BHFF has?",
                    FileIds = new List<string>() { responseUpload.Result!.Id }
                };

                HttpOperationResult<CreateMessageResponse> createMessageResult = await openAi.MessagesService.CreateAsync(createMessageRequest, CancellationToken.None);
                if (createMessageResult.IsSuccess)
                {
                    Console.WriteLine(createMessageResult.Result!);
                    Console.WriteLine();
                }

                // demo: how to query the list of messages
                MessageListRequest listRequest = new MessageListRequest()
                {
                    ThreadId = threadId
                };
                HttpOperationResult<MessageListResponse> queryListResult = await openAi.MessagesService.GetAsync(listRequest, CancellationToken.None);
                if (queryListResult.IsSuccess)
                {
                    Console.WriteLine(queryListResult.Result!.HasMore);
                    Console.WriteLine(queryListResult.Result!.FirstId);
                    Console.WriteLine(queryListResult.Result!.LastId);
                    foreach (MessageData messageData in queryListResult.Result!.Data)
                    {
                        Console.WriteLine(messageData.Id);
                    }
                    Console.WriteLine();
                }

                HttpOperationResult<FileDeleteResponse> responseDelete = openAi.FileService.DeleteFileAsync(responseUpload.Result!.Id, CancellationToken.None).GetAwaiter().GetResult();
                if (responseDelete.IsSuccess)
                {
                    Console.WriteLine(responseDelete.Result!);
                }
                else
                {
                    Console.WriteLine(responseDelete);
                }
            }

        }

    }

}
