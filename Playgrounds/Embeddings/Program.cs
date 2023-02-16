using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Embeddings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Embeddings
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

            EmbeddingsRequest request = new EmbeddingsRequest();
            request.InputTextsForEmbeddings.Add("The food was delicious and the waiter...");

            HttpOperationResult<EmbeddingsResponse> response = await openAi.EmbeddingsService.GetAsync(request, CancellationToken.None);

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