using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextEdits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestCompletions.Edit
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

            TextEditRequest request = new TextEditRequest();
            request.InputTextForEditing = "Do you happy with your order?";
            request.Instruction = "Fix the grammar";

            Console.WriteLine(request.InputTextForEditing);
            Console.WriteLine(request.Instruction);

            HttpOperationResult<TextEditResponse> response = await openAi.TextEditService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                response.Result!.Choices.ForEach(c => Console.WriteLine(c.Text)); // output: Are you happy with your order?
            }
            else
            {
                Console.WriteLine(response);
            }

        }

    }
}