using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Models
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
            HttpOperationResult<ModelsResponse> response = await openAi.ModelService.GetAsync().ConfigureAwait(false);

            if (response.IsSuccess)
            {
                string classText = KnownModelTypesClassGenerator.GenerateModelsLookup(response.Result!);
                Console.WriteLine(classText);
                File.WriteAllText("KnownModelTypes.cs", classText);
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }
}