using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.VectorStores;
using Forge.OpenAI.Models.VectorStoreFiles;
using Forge.OpenAI.Models.VectorStoreFileBatches;
using Forge.OpenAI.Models.Threads;
using Forge.OpenAI.Models.Messages;
using Forge.OpenAI.Models.Runs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Forge.OpenAI.Models.Shared;

namespace VectorStores
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can you create, configure and administrate a run with a thread and an assistant.
            // More information: https://platform.openai.com/docs/api-reference/assistants
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

            await VectorStoreTestAsync(openAi);

        }

        static async Task VectorStoreTestAsync(IOpenAIService openAi)
        {
            HttpOperationResult<CreateVectorStoreResponse> createResult = await openAi.VectorStoreService.CreateAsync(new CreateVectorStoreRequest() { Name = "My Vector Store" });
            if (createResult.IsSuccess)
            {
                HttpOperationResult<VectorStoreListResponse> listResult = await openAi.VectorStoreService.GetAsync(new VectorStoreListRequest());
                if (listResult.IsSuccess)
                {
                    foreach (VectorStoreData vectorStoreData in listResult.Result!.Data)
                    {
                        Console.WriteLine($"Vector store: {vectorStoreData.Id}");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to list vector stores");
                    Console.WriteLine(listResult);
                }

                HttpOperationResult<VectoreStoreResponse> getResult = await openAi.VectorStoreService.GetAsync(createResult.Result!.Id);
                if (getResult.IsSuccess)
                {
                    Console.WriteLine(getResult.Result!);
                }
                else
                {
                    Console.WriteLine("Unable to get vector store");
                    Console.WriteLine(getResult);
                }

                HttpOperationResult<VectoreStoreResponse> modificationResult = await openAi.VectorStoreService.ModifyAsync(new ModifyVectorStoreRequest(createResult.Result!.Id) { Name = "Modified vector store name" });
                if (modificationResult.IsSuccess)
                {
                    Console.WriteLine(modificationResult.Result!);
                }
                else
                {
                    Console.WriteLine("Unable to modify vector store");
                    Console.WriteLine(modificationResult);
                }

                if (listResult.IsSuccess)
                {
                    foreach (VectorStoreData vectorStoreData in listResult.Result!.Data)
                    {
                        HttpOperationResult<DeleteVectorStoreResponse> deleteResult = await openAi.VectorStoreService.DeleteAsync(vectorStoreData.Id);
                        Console.WriteLine(deleteResult.IsSuccess ? $"Vector store deleted: {deleteResult.Result!.Deleted}" : "Unable to delete vector store");
                    }
                }

            }
            else
            {
                Console.WriteLine("Unable to create vector store");
                Console.WriteLine(createResult);
            }

        }

    }

}
