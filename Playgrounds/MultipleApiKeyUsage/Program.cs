using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI;
using Forge.OpenAI.Authentication;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Services;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Forge.OpenAI.GPT;

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
            const string apiKeyForUserC = "";


            // 1, First example: create the OpenAI service instances for the users with manual DI configuration
            // AddForgeOpenAI can be replaced with other init methods, see ServiceCollectionExtensions.cs
            IOpenAIService openAiInstanceForUserA =
                OpenAIService
                    .CreateService(sc =>
                        sc.AddForgeOpenAI(options =>
                            options.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserA)), out ServiceProvider serviceProviderA);


            // 2, Second example: the same can be done with an action used to configure OpenAIOptions
            IOpenAIService openAiInstanceForUserB = OpenAIService.CreateService((OpenAIOptions options) =>
            {
                options.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserB);
            }, out ServiceProvider serviceProviderB);

            // 3, Third example: the same can be done with an own OpenAIOptions instance
            OpenAIOptions optionsForUserC = new OpenAIOptions();
            optionsForUserC.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserC);

            IOpenAIService openAiInstanceForUserC = OpenAIService.CreateService(optionsForUserC, out ServiceProvider serviceProviderC);


            // Now lets use the OpenAI instances
            using (serviceProviderA)
            {
                using (serviceProviderB)
                {
                    using (serviceProviderC)
                    {
                        // make actions with the OpenAI instances
                        await ChatWithStreamingModeWithCallback(openAiInstanceForUserA);
                        await ChatWithStreamingModeWithCallback(openAiInstanceForUserB);
                        await ChatWithStreamingModeWithCallback(openAiInstanceForUserC);

                        // NOTE: there is an other example in the Playgrounds here, which demonstrates
                        // how you can use the OpenAPI with multiple users, but with only one ApiKey
                        // This is useful, if you have multiple users, because it is highly recommended
                        // to differentiate them. If a user against the OpenAPI rules, this user will be
                        // denied and not your whole apiKey and your other users.
                    }
                }
            }

            // NOTE: if you do want to manage the lifecycle of the ServiceProvider instance, you can discard it, like that:
            // IOpenAIService openAiInstanceForUserC = OpenAIService.CreateService(optionsForUserC, out _);
            //
            // This is not a generally recommended way, because you can't dispose the ServiceProvider instance
            // and you can't release the resources, which are used by the OpenAI services.
            // The recommended way is to use the using statement, like in the example above.
            // If you don't want to use the using statement, you can store the ServiceProvider instance
            // and dispose it manually, when you don't need it anymore.
            // But in this case, you have to be careful, because you have to dispose the ServiceProvider instance
            // in the right time, otherwise you can have memory leaks.
            //
            // The only exception is, when you use the OpenAI services in a long running application, like a web application,
            // where you don't want to dispose the ServiceProvider instance, because you want to use it during the whole lifecycle
            // of the application. In this case, you have to be careful, because you have to dispose the ServiceProvider instance
            // when the application is shutting down, otherwise you can have memory leaks.

        }

        static async Task ChatWithStreamingModeWithCallback(IOpenAIService openAi)
        {
            // this method is useful for older .NET where the IAsyncEnumerable is not supported

            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser("Write a C# code which demonstrate how to open a text file and read its content"));
            request.MaxTokens = 4096 - GPT3Tokenizer.Encode(request.Messages[0].ContentAsString).Count; // calculating max token
            request.Temperature = 0.1; // lower value means more precise answer

            Console.WriteLine(request.Messages[0].Content);

            Action<HttpOperationResult<IAsyncEventInfo<ChatCompletionStreamedResponse>>> receivedDataHandler = (HttpOperationResult<IAsyncEventInfo<ChatCompletionStreamedResponse>> actionResponse) =>
            {
                if (actionResponse.IsSuccess)
                {
                    Console.Write(actionResponse.Result!.Data.Choices[0].Delta.Content);
                }
                else
                {
                    Console.WriteLine(actionResponse);
                }
            };

            HttpOperationResult response = await openAi.ChatCompletionService.GetStreamAsync(request, receivedDataHandler, CancellationToken.None);
            if (response.IsSuccess)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}