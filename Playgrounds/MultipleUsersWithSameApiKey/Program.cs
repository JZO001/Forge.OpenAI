using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextCompletions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultipleUsersWithSameApiKey
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how to use the API with multiple individual users,
            // but with the same apiKey.
            // This is useful, if you have multiple users, becuase it is highly recommended
            // to differentiate them. If a user against the OpenAPI rules, this user will be
            // denied and not your whole apiKey and your other users.
            //
            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s)

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    services.AddForgeOpenAI(options => {
                        options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
                    });
                })
                .Build();

            IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;


            // use a unique identifier for your users
            // it can be an email, a guid, etc...
            const string idForUserA = "user_a@email.com";
            const string idForUserB = "user_b@email.com";

            // works with User "A"
            await TextEditExampleAsync(openAi, idForUserA);

            // works with User "B"
            await TextEditExampleAsync(openAi, idForUserB);
        }

        static async Task TextEditExampleAsync(IOpenAIService openAIService, string userId)
        {
            TextCompletionRequest request = new TextCompletionRequest();
            request.Prompt = "Say this is a test";
            request.User = userId;

            Console.WriteLine(request.Prompt);

            HttpOperationResult<TextCompletionResponse> response = 
                await openAIService.TextCompletionService
                    .GetAsync(request, CancellationToken.None);
            
            if (response.IsSuccess)
            {
                Console.WriteLine();
                response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}