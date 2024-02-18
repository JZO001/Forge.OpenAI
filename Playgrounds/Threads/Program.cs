using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Threads;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Threads
{

    /// <summary>
    /// Resources:
    /// https://platform.openai.com/docs/api-reference/assistants
    /// https://platform.openai.com/docs/assistants/overview
    /// </summary>
    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can you create, configure and administrate a thread.
            // More information: https://platform.openai.com/docs/api-reference/threads
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
            CreateThreadRequest request = new CreateThreadRequest()
            {
                Messages = new List<Message>()
                {
                    new Message("Hello, how are you?")
                }
            };

            HttpOperationResult<CreateThreadResponse> createResult = await openAi.ThreadsService.CreateAsync(request, CancellationToken.None);
            if (createResult.IsSuccess)
            {
                Console.WriteLine(createResult.Result!);
                Console.WriteLine();

                // demo: how to query the thread by id
                HttpOperationResult<RetrieveThreadResponse> queryResult = await openAi.ThreadsService.GetAsync(createResult.Result!.Id, CancellationToken.None);
                if (queryResult.IsSuccess)
                {
                    Console.WriteLine(queryResult.Result!);
                    Console.WriteLine();
                }

                // demo: change the parameters of a thread
                ModifyThreadRequest modifyRequest = new ModifyThreadRequest()
                {
                    ThreadId = createResult.Result!.Id,
                    Metadata = new Dictionary<string, string>()
                    {
                        { "modified", "true" },
                        { "user", "abc123" }
                    }
                };
                HttpOperationResult<ModifyThreadResponse> modifyResult = await openAi.ThreadsService.ModifyAsync(modifyRequest, CancellationToken.None);
                if (modifyResult.IsSuccess)
                {
                    Console.WriteLine(modifyResult.Result!);
                    Console.WriteLine();
                }

                // demo: delete your thread
                HttpOperationResult<DeleteThreadResponse> deleteResult = await openAi.ThreadsService.DeleteAsync(createResult.Result!.Id, CancellationToken.None);
                if (deleteResult.IsSuccess)
                {
                    Console.WriteLine($"Deleted: {deleteResult.Result!.Deleted}");
                }

            }
            else
            {
                Console.WriteLine("Unable to create thread");
                Console.WriteLine(createResult);
            }

        }

    }

}
