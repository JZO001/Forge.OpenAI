using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Models
{
#pragma warning disable S1118 // Utility classes should not have public constructors
    internal class Program
#pragma warning restore S1118 // Utility classes should not have public constructors
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can query the available OpenAI models,
            // which can be used for different purposes.
            // You can find additional information here: https://platform.openai.com/docs/models/overview
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
            HttpOperationResult<ModelsResponse> response = await openAi.ModelService.GetAsync();

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