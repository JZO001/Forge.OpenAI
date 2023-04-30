using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Images;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Images.Variations
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can ask OpenAI to make variation(s) from
            // an existing image you provide.
            // More information: https://platform.openai.com/docs/guides/images/variations
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

            ImageVariationRequest request = new ImageVariationRequest();
            request.Image = new BinaryContentData() { ContentName = "Original Image", SourceStream = File.OpenRead("image_original.png") };
            request.NumberOfVariationImages = 2; // create 2 variations
            
            using (request.Image.SourceStream)
            {
                HttpOperationResult<ImageVariationResponse> response = await openAi.ImageService.VariateImageAsync(request, CancellationToken.None);
                if (response.IsSuccess)
                {
                    Console.WriteLine(response.Result!);

                    response.Result!.ImageData.ForEach(imageData => OpenUrl(imageData.ImageUrl));
                }
                else
                {
                    Console.WriteLine(response);
                }
            }
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

    }

}