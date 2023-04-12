using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.ChatCompletions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatCompletions
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how to make a simple conversation with ChatGPT
            //
            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s)

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    services.AddForgeOpenAI(options =>
                    {
                        options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
                    });
                })
                .Build();

            IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

            await ChatWithNonStreamingModeAsync(openAi);

            await ChatWithStreamingModeWithCallback(openAi);

            await ChatWithStreamingMode(openAi);
        }

        static async Task ChatWithNonStreamingModeAsync(IOpenAIService openAi)
        {
            // in this scenario the answer generated on server side, than the whole chat message will be sent in one pass

            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser("Count to 20, with a comma between each number and no newlines. E.g., 1, 2, 3, ..."));

            HttpOperationResult<ChatCompletionResponse> response = await openAi.ChatCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                Console.WriteLine();
                response.Result!.Choices.ForEach(c => Console.WriteLine(c.Message.Content));

                Console.WriteLine();

                request.Messages.Add(response.Result!.Choices[0].Message);
                request.Messages.Add(ChatMessage.CreateFromUser("Please count from 21 to 30, on the same way than previously."));

                response = await openAi.ChatCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Result!.Choices.ForEach(c => Console.WriteLine(c.Message.Content));
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

        static async Task ChatWithStreamingModeWithCallback(IOpenAIService openAi)
        {
            // this method is useful for older .NET where the IAsyncEnumerable is not supported

            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser("Write a C# code which demonstrate how to open a text file and read its content"));
            request.MaxTokens = 4096 - request.Messages[0].Content.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length - 100; // calculating max token
            request.Temperature = 0.1; // lower value means more precise answer

            Console.WriteLine(request.Messages[0].Content);

            Action<HttpOperationResult<ChatCompletionStreamedResponse>> receivedDataHandler = (HttpOperationResult<ChatCompletionStreamedResponse> response) =>
            {
                if (response.IsSuccess)
                {
                    Console.Write(response.Result?.Choices[0].Delta.Content);
                }
                else
                {
                    Console.WriteLine(response);
                }
            };

            HttpOperationResult response = await openAi.ChatCompletionService.GetStreamAsync(request, receivedDataHandler, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(response);
            }
        }

        static async Task ChatWithStreamingMode(IOpenAIService openAi)
        {
            Console.ReadKey();

            ChatCompletionRequest request = new ChatCompletionRequest(ChatMessage.CreateFromUser("Write a C# code which demonstrate how to write some text into file"));
            request.MaxTokens = 4096 - request.Messages[0].Content.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length - 100; // calculating max token
            request.Temperature = 0.1; // lower value means more precise answer

            Console.WriteLine(request.Messages[0].Content);

            await foreach (HttpOperationResult<ChatCompletionStreamedResponse> response in openAi.ChatCompletionService.GetStreamAsync(request, CancellationToken.None))
            {
                if (response.IsSuccess)
                {
                    Console.Write(response.Result?.Choices[0].Delta.Content);
                }
                else
                {
                    Console.WriteLine(response);
                }
            }

        }

    }

}