using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Audio.Transcription;
using Forge.OpenAI.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Transcription
{

    internal class Program
    {
        public static string DecodeFromUtf16ToUtf8(string utf16String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf16String.Length];
            for (int i = 0; i < utf16String.Length; ++i)
                utf8Bytes[i] = (byte)utf16String[i];

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

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
            //request.Language = "ta";
            HttpOperationResult<TranscriptionResponse> response = await openAi.TranscriptionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(response.Result?.Text);
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}