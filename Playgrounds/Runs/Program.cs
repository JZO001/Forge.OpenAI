using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using Forge.OpenAI.Models.Threads;
using Forge.OpenAI.Models.Messages;
using Forge.OpenAI.Models.Runs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Forge.OpenAI.Models.Shared;

namespace Runs
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can you create, configure and administrate a run with a thread and an assistant.
            // More information: https://platform.openai.com/docs/api-reference/assistants
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

            await CreateRunAsStream(openAi);

            await CreateThreadAndRunAsStream(openAi);
        }

        static async Task CreateRunAsStream(IOpenAIService openAi)
        {
            // example from: https://platform.openai.com/docs/assistants/overview/step-1-create-an-assistant

            // Create an assistant
            CreateAssistantRequest createAssistantRequest = new CreateAssistantRequest()
            {
                Model = KnownModelTypes.Gpt_4o,
                Name = "Math Tutor",
                Instructions = "You are a personal math tutor. Write and run code to answer math questions.",
                Tools = new List<Tool>()
                {
                    new Tool() { Type = Tool.CODE_INTERPRETER }
                }
            };

            // create the assistant
            HttpOperationResult<AssistantResponse> createAssistantResult = await openAi.AssistantService.CreateAsync(createAssistantRequest, CancellationToken.None);
            if (createAssistantResult.IsSuccess)
            {
                Console.WriteLine(createAssistantResult.Result!);
                Console.WriteLine();

                // create the thread
                CreateThreadRequest createThreadRequest = new CreateThreadRequest();

                HttpOperationResult<CreateThreadResponse> createThreadResult = await openAi.ThreadsService.CreateAsync(createThreadRequest, CancellationToken.None);
                if (createThreadResult.IsSuccess)
                {
                    Console.WriteLine(createThreadResult.Result!);
                    Console.WriteLine();

                    // Add a message to the thread
                    CreateMessageRequest createMessageRequest = new CreateMessageRequest()
                    {
                        ThreadId = createThreadResult.Result!.Id,
                        Content = "I need to solve the equation `3x + 11 = 14`. Can you help me?",
                        Role = CreateMessageRequest.ROLE_USER
                    };

                    HttpOperationResult<CreateMessageResponse> createMessageResult = await openAi.MessagesService.CreateAsync(createMessageRequest, CancellationToken.None);
                    if (createMessageResult.IsSuccess)
                    {
                        Console.WriteLine(createMessageResult.Result!);
                        Console.WriteLine();

                        CreateRunRequest createRunRequest = new CreateRunRequest()
                        {
                            AssistantId = createAssistantResult.Result!.Id,
                            ThreadId = createThreadResult.Result!.Id,
                            Instructions = "Please address the user as Jane Doe. The user has a premium account.",
                            Stream = true
                        };

                        HttpOperationResult streamResult = await openAi.RunService.CreateAsStreamAsync(createRunRequest, (run) =>
                        {
                            Console.WriteLine(run);
                        }, CancellationToken.None);

                    }

                    // delete your thread
                    await openAi.ThreadsService.DeleteAsync(createThreadResult.Result!.Id, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("Unable to create thread");
                    Console.WriteLine(createAssistantResult);
                }

                // delete the assistant
                await openAi.AssistantService.DeleteAsync(createAssistantResult.Result!.Id, CancellationToken.None);

            }
            else
            {
                Console.WriteLine("Unable to create assistant");
                Console.WriteLine(createAssistantResult);
            }
        }

        static async Task CreateThreadAndRunAsStream(IOpenAIService openAi)
        {
            // Create an assistant
            CreateAssistantRequest createAssistantRequest = new CreateAssistantRequest()
            {
                Model = KnownModelTypes.Gpt_4o,
                Name = "Math Tutor",
                Instructions = "You are a personal math tutor. Write and run code to answer math questions.",
                Tools = new List<Tool>()
                {
                    new Tool() { Type = Tool.CODE_INTERPRETER }
                }
            };

            // create the assistant
            HttpOperationResult<AssistantResponse> createAssistantResult = await openAi.AssistantService.CreateAsync(createAssistantRequest, CancellationToken.None);
            if (createAssistantResult.IsSuccess)
            {
                Console.WriteLine(createAssistantResult.Result!);
                Console.WriteLine();

                CreateThreadAndRunRequest createThreadAndRunRequest = new CreateThreadAndRunRequest()
                {
                    AssistantId = createAssistantResult.Result!.Id,
                    Model = KnownModelTypes.Gpt_4o,
                    Instructions = "You are a personal math tutor. Write and run code to answer math questions.",
                    Tools = new List<Tool>()
                    {
                        new Tool() { Type = Tool.CODE_INTERPRETER }
                    },
                    Thread = new CreateThreadRequest()
                    { 
                        Messages = new List<Message>()
                        {
                            new Message("I need to solve the equation `3x + 11 = 14`. Can you help me?")
                            {
                                Role = Message.ROLE_USER
                            }
                        }
                    },
                    Stream = true
                };

                HttpOperationResult streamResult = await openAi.RunService.CreateThreadAndRunAsStreamAsync(createThreadAndRunRequest, (run) =>
                {
                    Console.WriteLine(run);
                }, CancellationToken.None);

                // delete the assistant
                await openAi.AssistantService.DeleteAsync(createAssistantResult.Result!.Id, CancellationToken.None);

            }
            else
            {
                Console.WriteLine("Unable to create assistant");
                Console.WriteLine(createAssistantResult);
            }

        }

    }

}
