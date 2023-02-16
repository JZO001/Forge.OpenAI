using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextCompletions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TextCompletions.Complete
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

            await ConversationWithNonStreamingModeAsync(openAi);

            await ConversationWithStreamingModeWithCallback(openAi);

            await ConversationWithStreamingMode(openAi);
        }

        static async Task ConversationWithNonStreamingModeAsync(IOpenAIService openAi)
        {
            // in this scenario the answer generated on server side than the whole text will be sent in one pass
            // this method is useful for small conversatons and for short answers

            TextCompletionRequest request = new TextCompletionRequest();
            request.Prompt = "Say this is a test";

            HttpOperationResult<TextCompletionResponse> response = await openAi.TextCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));

                request.Prompt = "Are you sure?";
                response = await openAi.TextCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));
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

        static async Task ConversationWithStreamingModeWithCallback(IOpenAIService openAi)
        {
            // this method is useful for older .NET where the IAsyncEnumerable is not supported

            TextCompletionRequest request = new TextCompletionRequest();
            request.Prompt = "Write a C# code which demonstrate how to open a text file and read its content";
            request.MaxTokens = 4096 - request.Prompt.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
            request.Temperature = 0.1; // lower value means more precise answer

            Console.WriteLine(request.Prompt);

            Action<HttpOperationResult<TextCompletionResponse>> receivedDataHandler = (HttpOperationResult<TextCompletionResponse> response) => 
            {
                if (response.IsSuccess)
                {
                    Console.Write(response.Result?.Completions[0].Text);
                }
                else
                {
                    Console.WriteLine(response);
                }
            };

            HttpOperationResult response = await openAi.TextCompletionService.GetStreamAsync(request, receivedDataHandler, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(response);
            }
        }

        static async Task ConversationWithStreamingMode(IOpenAIService openAi)
        {
            TextCompletionRequest request = new TextCompletionRequest();
            request.Prompt = "Write a C# code which demonstrate how to write some text into file";
            request.MaxTokens = 4096 - request.Prompt.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
            request.Temperature = 0.1; // lower value means more precise answer

            Console.WriteLine(request.Prompt);

            await foreach (HttpOperationResult<TextCompletionResponse> response in openAi.TextCompletionService.GetStreamAsync(request, CancellationToken.None))
            {
                if (response.IsSuccess)
                {
                    Console.Write(response.Result?.Completions[0].Text);
                }
                else
                {
                    Console.WriteLine(response);
                }
            }

        }

    }

}