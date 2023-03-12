using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Audio.Transcription;
using Forge.OpenAI.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Transcription
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

            TranscriptionRequest request = new TranscriptionRequest();
            request.AudioFile = new BinaryContentData() { ContentName = "audio.mp3", SourceStream = File.OpenRead("audio.mp3") };

            HttpOperationResult<TranscriptionResponse> response = await openAi.TranscriptionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                Console.WriteLine(response.Result?.Text);
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}