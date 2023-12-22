using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Assistant
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
            // This example demonstrates, how you can you create, configure and administrate an assistant.
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

            Console.WriteLine("Creating assistant");
            Console.WriteLine();

            // Create an assistant
            CreateAssistantRequest request = new CreateAssistantRequest() 
            { 
                Model = KnownModelTypes.Gpt3_5Turbo,
                Name = "Math Tutor",
                Instructions = "You are a personal math tutor. When asked a question, write and run Python code to answer the question.",
                Tools = new List<Tool>() 
                { 
                    new Tool() { Type = Tool.CODE_INTERPRETER } 
                }
            };

            HttpOperationResult<AssistantResponse> createResult = await openAi.AssistantService.CreateAsync(request, CancellationToken.None);
            if (createResult.IsSuccess)
            {
                Console.WriteLine(createResult.Result!); 
                Console.WriteLine();

                // demo: hot to query the assistant by id
                HttpOperationResult<AssistantResponse> queryResult = await openAi.AssistantService.GetAsync(createResult.Result!.Id, CancellationToken.None);
                if (queryResult.IsSuccess)
                {
                    Console.WriteLine(queryResult.Result!);
                    Console.WriteLine();
                }

                // demo: how to query the list of assistants
                // you can also implement paging operations with the request
                AssistantListRequest listQueryRequest = new AssistantListRequest();
                HttpOperationResult<AssistantListResponse> listResult = await openAi.AssistantService.GetAsync(listQueryRequest, CancellationToken.None);
                if (listResult.IsSuccess)
                {
                    listResult.Result!.Data.ToList().ForEach(assistant =>
                    {
                        Console.WriteLine(assistant);
                        Console.WriteLine();
                    });
                }

                // demo: change the parameters of an assistant
                ModifyAssistantRequest modifyRequest = new ModifyAssistantRequest()
                {
                    AssistantId = createResult.Result!.Id,
                    Model = KnownModelTypes.Gpt3_5Turbo,
                    Name = "Math Tutor 2",
                    Instructions = "You are a personal math tutor. When asked a question, write and run Python code to answer the question.",
                    Tools = new List<Tool>()
                    {
                        new Tool() { Type = Tool.CODE_INTERPRETER } 
                    }
                };
                HttpOperationResult<AssistantResponse> modifyResult = await openAi.AssistantService.ModifyAsync(modifyRequest, CancellationToken.None);
                if (modifyResult.IsSuccess)
                {
                    Console.WriteLine(modifyResult.Result!);
                    Console.WriteLine();
                }

                // demo: delete your assistant
                HttpOperationResult<DeleteStateResponse> deleteResult = await openAi.AssistantService.DeleteAsync(createResult.Result!.Id, CancellationToken.None);
                if (deleteResult.IsSuccess)
                {
                    Console.WriteLine($"Deleted: {deleteResult.Result!.Deleted}");
                }

            }
            else
            {
                Console.WriteLine("Unable to create assistant");
                Console.WriteLine(createResult);
            }

        }

    }

}
