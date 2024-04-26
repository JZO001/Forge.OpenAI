using Forge.OpenAI;
using Forge.OpenAI.Authentication;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextEdits;
using Forge.OpenAI.Services;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MultipleApiKeyUsage
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how to use the Forge.OpenAI without dependency injection
            // and create service instances for individual users which have different OpenAI API key.

            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s), for example let's create two for this demo.

            // Add the created API keys here
            const string apiKeyForUserA = "";
            const string apiKeyForUserB = "";

            // Create the OpenAI service instances for the users with manual DI configuration
            // AddForgeOpenAI can be replaced with other init methods, see ServiceCollectionExtensions.cs
            IOpenAIService openAiInstanceForUserA = 
                OpenAIService
                    .CreateService(sc => 
                        sc.AddForgeOpenAI(options => 
                            options.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserA)), out ServiceProvider serviceProviderA);

            // The same can be done with the OpenAIOptions
            OpenAIOptions optionsForUserB = new OpenAIOptions();
            optionsForUserB.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserB);

            IOpenAIService openAiInstanceForUserB = OpenAIService.CreateService((OpenAIOptions options) => 
            {
                options.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserB);
            }, out ServiceProvider serviceProviderB);

            using (serviceProviderA)
            {
                using (serviceProviderB)
                {
                    await TextEditExampleAsync(openAiInstanceForUserA);
                    await TextEditExampleAsync(openAiInstanceForUserB);

                    // NOTE: there is an other example in the Playgrounds here, which demonstrates
                    // how you can use the OpenAPI with multiple users, but with only one ApiKey
                    // This is useful, if you have multiple users, becuase it is highly recommended
                    // to differentiate them. If a user against the OpenAPI rules, this user will be
                    // denied and not your whole apiKey and your other users.
                }
            }
        }

        static async Task TextEditExampleAsync(IOpenAIService openAIService)
        {
            TextEditRequest request = new TextEditRequest();
            request.InputTextForEditing = "Do you happy with your order?";
            request.Instruction = "Fix the grammar";

            Console.WriteLine(request.InputTextForEditing);
            Console.WriteLine(request.Instruction);

            HttpOperationResult<TextEditResponse> response = 
                await openAIService.TextEditService.GetAsync(request, CancellationToken.None);
            
            if (response.IsSuccess)
            {
                response.Result!.Choices.ToList().ForEach(c => Console.WriteLine(c.Text)); // output: Are you happy with your order?
            }
            else
            {
                Console.WriteLine(response);
            }

        }

    }

}