using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Audio.Translation;
using Forge.OpenAI.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Translation
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

            TranslationRequest request = new TranslationRequest();
            request.AudioFile = new BinaryContentData() { ContentName = "audio.mp3", SourceStream = File.OpenRead("audio.mp3") };

            HttpOperationResult<TranslationResponse> response = await openAi.TranslationService.GetAsync(request, CancellationToken.None);
            if (response.IsSuccess)
            {
                // text: "How can I motivate you?"
                Console.WriteLine(response.Result?.Text);
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}