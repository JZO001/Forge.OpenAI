using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Models.Common;
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
            await ChatWithNonStreamingModeAsync(openAi, idForUserA);

            // works with User "B"
            await ChatWithNonStreamingModeAsync(openAi, idForUserB);
        }

        static async Task ChatWithNonStreamingModeAsync(IOpenAIService openAi, string userId)
        {
            // in this scenario the answer generated on server side, than the whole chat message will be sent in one pass

            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser("Count to 20, with a comma between each number and no newlines. E.g., 1, 2, 3, ..."));
            request.User = userId;

            HttpOperationResult<ChatCompletionResponse> response = await openAi.ChatCompletionService.GetAsync(request, CancellationToken.None);
            if (response.IsSuccess)
            {
                Console.WriteLine();
                response.Result.Choices.ToList().ForEach(c => Console.WriteLine(c.Message.Content));

                Console.WriteLine();

                request.Messages.Add(response.Result.Choices[0].Message);
                request.Messages.Add(ChatMessage.CreateFromUser("Please count from 21 to 30, on the same way than previously."));

                response = await openAi.ChatCompletionService.GetAsync(request, CancellationToken.None);
                if (response.IsSuccess)
                {
                    response.Result.Choices.ToList().ForEach(c => Console.WriteLine(c.Message.Content));
                }
                else
                {
                    Console.WriteLine(response);
                }
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}