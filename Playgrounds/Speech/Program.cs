using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Audio.Speech;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Forge.OpenAI.Models;

namespace Speech
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

            SpeechRequest request = new SpeechRequest() 
            { 
                Model = KnownModelTypes.Tts_1,
                Input = "Generates audio from the input text.",
                ResponseFormat = SpeechRequest.RESPONSE_FORMAT_MP3,
                Speed = 1.0f,
                Voice = SpeechRequest.VOICE_ALLOY
            };

            using MemoryStream stream = new MemoryStream();
            HttpOperationResult<Stream> response = await openAi.SpeechService.CreateSpeechAsync(request, stream, CancellationToken.None);
            if (response.IsSuccess)
            {
                using FileStream fileStream = new FileStream("speech.mp3", FileMode.Create, FileAccess.Write);
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);
            }
            else
            {
                Console.WriteLine(response);
            }
        }

    }

}
