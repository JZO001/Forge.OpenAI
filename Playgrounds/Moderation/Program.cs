using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Moderations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Moderation
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how to check, if a set of text can pass the OpenAI moderation rules.
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

            ModerationRequest request = new ModerationRequest(new string[] { "I want to kill them." });
            HttpOperationResult<ModerationResponse> response = await openAi.ModerationService.GetAsync(request, CancellationToken.None);

            if (response.IsSuccess)
            {
                Console.WriteLine(response.Result!);
            }
            else
            {
                Console.WriteLine(response);
            }

        }

    }

}