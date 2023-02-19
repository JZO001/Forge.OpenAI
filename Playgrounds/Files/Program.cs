using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Files
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can upload, delete or query a file.
            // This feature is useful for fine tune, search, etc
            // More information: https://platform.openai.com/docs/api-reference/files
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

            Console.WriteLine("Uploading file...");
            Console.WriteLine();

            FileUploadRequest uploadRequest = new FileUploadRequest();
            uploadRequest.File = new BinaryContentData() { ContentName = "training", SourceStream = File.OpenRead("training.jsonl") };
            uploadRequest.Purpose = "fine-tune";

            HttpOperationResult<FileUploadResponse> responseUpload = await openAi.FileService.UploadFileAsync(uploadRequest, CancellationToken.None).ConfigureAwait(false);
            if (responseUpload.IsSuccess)
            {
                Console.WriteLine(responseUpload.Result!);
                Console.WriteLine();
                Console.WriteLine("Get file list");
                Console.WriteLine();

                HttpOperationResult<FileListResponse> fileListResult = await openAi.FileService.GetFileListAsync(CancellationToken.None).ConfigureAwait(false);

                if (fileListResult.IsSuccess)
                {
                    Console.WriteLine(fileListResult.Result!);
                    Console.WriteLine();

                    Console.WriteLine("Retrieve file(s) data");
                    Console.WriteLine();

                    fileListResult.Result!.Files.ForEach(fileData =>
                    {
                        Console.WriteLine($"Retrieving file data, id: {fileData.Id}");
                        Console.WriteLine();

                        HttpOperationResult<FileDataResponse> responseFileData = openAi.FileService.GetFileDataAsync(fileData.Id, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
                        if (responseFileData.IsSuccess)
                        {
                            Console.WriteLine(responseFileData.Result!);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine(responseFileData);
                            Console.WriteLine();
                        }
                    });

                    Console.WriteLine("Downloading file(s)...");
                    Console.WriteLine();

                    fileListResult.Result!.Files.ForEach(fileData =>
                    {
                        Console.WriteLine($"Downloading file, id: {fileData.Id}");
                        Console.WriteLine();

                        using (FileStream fs = new FileStream(fileData.Id, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            HttpOperationResult<Stream> responseFileDownload = openAi.FileService.DownloadFileAsync(fileData.Id, fs, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
                            if (responseFileDownload.IsSuccess)
                            {
                                Console.WriteLine("File successfully downloaded.");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(responseFileDownload);
                                Console.WriteLine();
                            }
                        }
                    });

                    Console.WriteLine("Delete file(s)");
                    Console.WriteLine();

                    fileListResult.Result!.Files.ForEach(fileData =>
                    {
                        Console.WriteLine($"Deleting file, id: {fileData.Id}");
                        Console.WriteLine();

                        HttpOperationResult<FileDeleteResponse> responseDelete = openAi.FileService.DeleteFileAsync(fileData.Id, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
                        if (responseDelete.IsSuccess)
                        {
                            Console.WriteLine(responseDelete.Result!);
                        }
                        else
                        {
                            Console.WriteLine(responseDelete);
                        }
                    });

                }
                else
                {
                    Console.WriteLine(responseUpload);
                }
            }
            else
            {
                Console.WriteLine(responseUpload);
            }

        }

    }

}