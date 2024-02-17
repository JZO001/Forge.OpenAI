using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Azure_OpenAI_Setup_Example
{

    internal class Program
    {

        static void Main(string[] args)
        {
            // This example demonstrates, how to setup the service with Azure-OpenAI provider
            //
            // Prerequisites: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/quickstart?tabs=command-line&pivots=programming-language-studio
            // Documentation: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference

            // setup the configuration
            OpenAIDefaultOptions.DefaultAzureResourceName = "YourAzureResourceName";
            OpenAIDefaultOptions.DefaultAzureDeploymentId = "YourAzureDeploymentId";

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    OpenAIOptions settings = builder.Configuration.GetSection(OpenAIOptions.ConfigurationSectionName).Get<OpenAIOptions>()!;

                    services.AddForgeAzureOpenAI(options =>
                    {
                        options.AuthenticationInfo = settings.AuthenticationInfo;

                        // optionally, you can override here also these settings
                        //options.AzureResourceName = settings.AzureResourceName;
                        //options.AzureDeploymentId = settings.AzureDeploymentId;
                        //options.BaseAddress = string.Format("https://{0}.openai.azure.com", "YourAzureResourceName");
                    });
                })
                .Build();

            IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

            // do something with the service

        }

    }

}