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
            HttpOperationResult<ModerationResponse> response = await openAi.ModerationService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);

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