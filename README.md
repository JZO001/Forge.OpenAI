# Forge.OpenAI
OpenAI API client library for .NET. This is not an official library, I was developed it for myself, for public and it is free to use.
Supported .NET versions:

x >= v4.6.1,

x >= Netstandard 2.0,

x >= dotNetCore 3.1,

.NET 6.0,

.NET 7.0



## Installing

To install the package add the following line to you csproj file replacing x.x.x with the latest version number:

```
<PackageReference Include="Forge.OpenAI" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package Forge.OpenAI
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

## Setup

You should create an ApiKey to work with the OpenAI API.

If you do not have an account at OpenAI, create one here:
https://platform.openai.com/

Than navigate to:
https://platform.openai.com/account/api-keys



By default, this library uses Microsoft Dependency Injection, however it is not necessary.

You can register the client services with the service collection in your _Startup.cs_ / _Program.cs_ file in your application.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddForgeOpenAI(options => {
        options.AuthenticationInfo = Configuration["OpenAI:ApiKey"]!;
    });
}
``` 

Or in your _Program.cs_ file.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddForgeOpenAI(options => {
        options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
    });

    await builder.Build().RunAsync();
}
```

Or

```c#
public static async Task Main(string[] args)
{
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();
}
```

You should provide your OpenAI API key and optionally your organization to boot up the service.
If you do not provide it in the configuration, service automatically lookup the necessary
information in your environment variables, in a Json file (.openai) or
in an environment file (.env).

Example for environment variables:

OPENAI_KEY or OPENAI_API_KEY or OPENAI_SECRET_KEY or TEST_OPENAI_SECRET_KEY are checked for the API key

ORGANIZATION key checked for the organzation


Example for Json file:

{
    "apikey": "your_api_key",
    "organization": "organization_id"
}


Environment file must contains key/value pairs in this format {key}={value}

For the '_key_', use one of the same value which described in Environment Variables above.

Example for environment file:

OPENAI_KEY=your_api_key

ORGANIZATION=optionally_your_organization


## Options

OpenAI and the dependent services require OpenAIOptions, which can be provided manually or it will happened,
if you use dependency injection. If you need to use multiple OpenAI service instances at the same time,
you should provide this options individually with different settings and authentication credentials.

In the options there are many Uri settings, which was not touched normally. The most important option
is the AuthenticationInfo property, which contains the ApiKey and and Organization Id.

Also, there is an additional option, called HttpMessageHandlerFactory, which constructs the HttpMessageHandler
for the HttpClient in some special cases, for example, if you want to override some behavior of the HttpClient.

There is a built-in logging feature, just for testing and debugging purposes, called LogRequestsAndResponses,
which persists all of requests and responses in a folder (LogRequestsAndResponsesFolder). With this feature,
you can check the low level messages. I do not recommend to use it in production environment.


## Examples

If you would like to learn more about the API capabilities, please visit https://platform.openai.com/docs/api-reference
If you need to generate an API key, please visit: https://platform.openai.com/account/api-keys

I have created a playground, which is part of this solution. It covers all of the features, which this library provides.
Feel free to run through these examples and play with the settings.

Also here is the OpenAI playground, where you can also find examples about the usage:
https://platform.openai.com/playground/p/default-chat?lang=node.js&mode=complete&model=text-davinci-003


## ApiKey usage #1: many users can use the same apiKey

This example demonstrates, how to use the API with multiple individual users, but with the same apiKey.
This is useful, if you have multiple users, becuase it is highly recommended
to differentiate them. If a user against the OpenAPI rules, this user will be
denied and not your whole apiKey and your other users.


```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how to use the API with multiple individual users,
    // but with the same apiKey.
    // This is useful, if you have multiple users, becuase it is highly recommended
    // to differentiate them. If a user against the OpenAPI rules, this user will be
    // denied and not your whole apiKey and your other users.
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;


    // use a unique identifier for your users
    // it can be an email, a guid, etc...
    const string idForUserA = "user_a@email.com";
    const string idForUserB = "user_b@email.com";

    // works with User "A"
    await TextEditExampleAsync(openAi, idForUserA);

    // works with User "B"
    await TextEditExampleAsync(openAi, idForUserB);
}

static async Task TextEditExampleAsync(IOpenAIService openAIService, string userId)
{
    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Say this is a test";
    request.User = userId;

    Console.WriteLine(request.Prompt);

    HttpOperationResult<TextCompletionResponse> response = 
        await openAIService.TextCompletionService
            .GetAsync(request, CancellationToken.None)
                .ConfigureAwait(false);
            
    if (response.IsSuccess)
    {
        Console.WriteLine();
        response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));
    }
    else
    {
        Console.WriteLine(response);
    }
}
```


## ApiKey usage #2: many users with different apiKey

This example demonstrates, how to use the Forge.OpenAI without dependency injection
and create service instances for individual users which have different OpenAI API key.


```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how to use the Forge.OpenAI without dependency injection
    // and create service instances for individual users which have different OpenAI API key.

    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s), for example let's create two for this demo.

    // Add the created API keys here
    const string apiKeyForUserA = "";
    const string apiKeyForUserB = "";

    OpenAIOptions optionsForUserA = new OpenAIOptions();
    optionsForUserA.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserA);

    OpenAIOptions optionsForUserB = new OpenAIOptions();
    optionsForUserB.AuthenticationInfo = new AuthenticationInfo(apiKeyForUserB);

    IOpenAIService openAiInstanceForUserA = OpenAIService.CreateService(optionsForUserA);
    IOpenAIService openAiInstanceForUserB = OpenAIService.CreateService(optionsForUserB);

    await TextEditExampleAsync(openAiInstanceForUserA);
    await TextEditExampleAsync(openAiInstanceForUserB);

    // NOTE: there is an other example in the Playgrouns here, which demonstrates
    // how you can use the OpenAPI with multiple users, but with only one ApiKey
    // This is useful, if you have multiple users, becuase it is highly recommended
    // to differentiate them. If a user against the OpenAPI rules, this user will be
    // denied and not your whole apiKey and your other users.
}

static async Task TextEditExampleAsync(IOpenAIService openAIService)
{
    TextEditRequest request = new TextEditRequest();
    request.InputTextForEditing = "Do you happy with your order?";
    request.Instruction = "Fix the grammar";

    Console.WriteLine(request.InputTextForEditing);
    Console.WriteLine(request.Instruction);

    HttpOperationResult<TextEditResponse> response = 
        await openAIService.TextEditService
            .GetAsync(request, CancellationToken.None)
                .ConfigureAwait(false);
            
    if (response.IsSuccess)
    {
        // output: Are you happy with your order?
        response.Result!.Choices.ForEach(c => Console.WriteLine(c.Text));
    }
    else
    {
        Console.WriteLine(response);
    }

}
```



## Example - Text completion 1.

The next code demonstrates how to give a simple instruction (prompt). The whole answer generated on the OpenAI side remotelly,
than the answer will send in a response.

```c#
public static async Task Main(string[] args)
{
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder
                    .Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    // in this scenario the answer generated on server side, 
    // than the whole text will be sent in one pass.
    // this method is useful for small conversatons and for short answers

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Say this is a test";

    HttpOperationResult<TextCompletionResponse> response = 
        await openAi.TextCompletionService
            .GetAsync(request, CancellationToken.None)
                .ConfigureAwait(false);

    if (response.IsSuccess)
    {
        response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));

        request.Prompt = "Are you sure?";

        response = await openAi.TextCompletionService
            .GetAsync(request, CancellationToken.None).ConfigureAwait(false);

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
```


## Example - Text completion 2.

The next example demonstrates, how you can receive an answer in streamed mode. Streamed mode means, you will get the generated answer
in pieces and not in one packages like in the previous example. Because of generating an answer takes time, it can be useful,
if you see the result in the meantime. The process also can be cancelled.

This version works with a callback. It will be called each time, if a piece of answer arrived.

```c#
public static async Task Main(string[] args)
{
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder
                    .Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    // this method is useful for older .NET where the IAsyncEnumerable is not supported,
    // or you just simply does not prefer this way

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Write a C# code which demonstrate how to open a text file and read its content";
    request.MaxTokens = 4096 - request.Prompt
        .Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
    request.Temperature = 0.1; // lower value means more precise answer

    Console.WriteLine(request.Prompt);

    Action<HttpOperationResult<TextCompletionResponse>> receivedDataHandler = 
        (HttpOperationResult<TextCompletionResponse> response) => 
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

    HttpOperationResult response = await openAi.TextCompletionService
        .GetStreamAsync(request, receivedDataHandler, CancellationToken.None)
            .ConfigureAwait(false);

    if (response.IsSuccess)
    {
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine(response);
    }

}
```


## Example - Text completion 3.

The last example in this topic demonstrates, how you can receive an answer in streamed mode also.

This version works with IAsyncEnumerable. It is not supported in older .NET versions.

```c#
public static async Task Main(string[] args)
{
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder
                    .Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Write a C# code which demonstrate how to write some text into file";
    request.MaxTokens = 4096 - request.Prompt
        .Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
    request.Temperature = 0.1; // lower value means more precise answer

    Console.WriteLine(request.Prompt);

    await foreach (HttpOperationResult<TextCompletionResponse> response in 
        openAi.TextCompletionService.GetStreamAsync(request, CancellationToken.None))
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
```


## Example - Text edit

Edit a text means something like that we ask the model to fix an incorrect sentence for example.

```c#
public static async Task Main(string[] args)
{
    using var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddForgeOpenAI(options => {
                options.AuthenticationInfo = builder
                    .Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    TextEditRequest request = new TextEditRequest();
    request.InputTextForEditing = "Do you happy with your order?";
    request.Instruction = "Fix the grammar";

    Console.WriteLine(request.InputTextForEditing);
    Console.WriteLine(request.Instruction);

    HttpOperationResult<TextEditResponse> response = 
        await openAi.TextEditService.GetAsync(request, CancellationToken.None)
            .ConfigureAwait(false);

    if (response.IsSuccess)
    {
        // output: Are you happy with your order?
        response.Result!.Choices.ForEach(c => Console.WriteLine(c.Text));
    }
    else
    {
        Console.WriteLine(response);
    }

}
```

## Image interaction API

The Images API provides three methods for interacting with images:

Creating images from scratch based on a text prompt
Creating edits of an existing image based on a new text prompt
Creating variations of an existing image

### Example - Create an image

Learn how to generate images with DALL·E models


```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how you can ask OpenAI to an image based on your instructions.
    // More information: https://platform.openai.com/docs/guides/images/image-generation-beta
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddForgeOpenAI(options => {
            options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
        });
    })
    .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    ImageCreateRequest request = new ImageCreateRequest();
    request.Prompt = "A cute baby sea otter";

    HttpOperationResult<ImageCreateResponse> response = 
        await openAi.ImageService
            .CreateImageAsync(request, CancellationToken.None)
                .ConfigureAwait(false);
    
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
```

### Example - Edit an image

Manipulate images with DALL·E models

The image edits endpoint allows you to edit and extend an image by uploading a mask. 
The transparent areas of the mask indicate where the image should be edited, 
and the prompt should describe the full new image, not just the erased area. 

```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how you can ask OpenAI to edit an existing image you provide.
    // More information: https://platform.openai.com/docs/guides/images/edits
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddForgeOpenAI(options => {
            options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
        });
    })
    .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    // Images should be in png format with ARGB

    ImageEditRequest request = new ImageEditRequest();
    request.Image = new BinaryContentData() 
    { 
        ContentName = "Original Image", 
        SourceStream = File.OpenRead("image_edit_original.png") 
    };
            
    using (request.Image.SourceStream)
    {
        request.Mask = new BinaryContentData() 
        { 
            ContentName = "Mask Image", 
            SourceStream = File.OpenRead("image_edit_mask.png") 
        };
        
        using (request.Mask.SourceStream)
        {
            request.Prompt = "A boy cycling away on a bicycle on the road";

            HttpOperationResult<ImageEditResponse> response = 
                await openAi.ImageService
                    .EditImageAsync(request, CancellationToken.None)
                        .ConfigureAwait(false);
            
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
}
```

### Example - Make variations from an image

The image variations endpoint allows you to generate a variation of a given image.

```c#
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
    request.Image = new BinaryContentData() 
    { 
        ContentName = "Original Image", 
        SourceStream = File.OpenRead("image_original.png") 
    };
    request.NumberOfVariationImages = 2; // create 2 variations
            
    using (request.Image.SourceStream)
    {
        HttpOperationResult<ImageVariationResponse> response = 
            await openAi.ImageService
                .VariateImageAsync(request, CancellationToken.None)
                    .ConfigureAwait(false);
        
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
```


## Acquire the available models using OoenAI API

The OpenAI API is powered by a family of models with different capabilities and price points. 
You can also customize the base models for your specific use case with fine-tuning.

More info: https://platform.openai.com/docs/models/models

```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how you can query the available OpenAI models,
    // which can be used for different purposes.
    // You can find additional information here: https://platform.openai.com/docs/models/overview
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddForgeOpenAI(options => {
            options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
        });
    })
    .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;
    HttpOperationResult<ModelsResponse> response = 
        await openAi.ModelService.GetAsync()
            .ConfigureAwait(false);

    if (response.IsSuccess)
    {
        string classText = KnownModelTypesClassGenerator
            .GenerateModelsLookup(response.Result!);

        Console.WriteLine(classText);

        File.WriteAllText("KnownModelTypes.cs", classText);
    }
    else
    {
        Console.WriteLine(response);
    }
}
```


## Fine-tune or "Teach" an OpenAI model using the API

Manage fine-tuning jobs to tailor a model to your specific training data.

More info: https://platform.openai.com/docs/api-reference/fine-tunes

The following example demonstrates all functions which are related to the fine tune operations.

```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how you can fine tune a model with the information you provide.
    // More information: https://platform.openai.com/docs/guides/fine-tuning
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

    Console.WriteLine("Uploading training file...");

    FileUploadRequest uploadRequest = new FileUploadRequest();
    uploadRequest.File = new BinaryContentData() 
    { 
        ContentName = "training", 
        SourceStream = File.OpenRead("training.jsonl") 
    };
    uploadRequest.Purpose = "fine-tune";

    HttpOperationResult<FileUploadResponse> responseUpload = 
        await openAi.FileService
            .UploadFileAsync(uploadRequest, CancellationToken.None)
                .ConfigureAwait(false);
    
    if (responseUpload.IsSuccess)
    {
        Console.WriteLine($"Uploaded, id: {responseUpload.Result!.Id}");
        Console.WriteLine("Creating fine tune job");
        Console.WriteLine();

        FineTuneCreateRequest createRequest = new FineTuneCreateRequest();
        createRequest.TrainingFileId = responseUpload.Result!.Id;

        HttpOperationResult<FineTuneCreateResponse> createResponse = 
            await openAi.FineTuneService
                .CreateAsync(createRequest, CancellationToken.None)
                    .ConfigureAwait(false);
        
        if (createResponse.IsSuccess)
        {
            Console.WriteLine($"Job created, id: {createResponse.Result!.Id}");
            Console.WriteLine("List fine tune jobs");
            Console.WriteLine();

            HttpOperationResult<FineTuneListResponse> listResponse = 
                await openAi.FineTuneService
                    .GetAsync(CancellationToken.None)
                        .ConfigureAwait(false);
            
            if (listResponse.IsSuccess)
            {
                listResponse.Result!.Jobs.ForEach(job =>
                {
                    Console.WriteLine(job);
                    Console.WriteLine();
                });
            }
            else
            {
                Console.WriteLine(listResponse);
            }

            Console.WriteLine();
            Console.WriteLine($"Retrieve fine tune job data, id: {createResponse.Result!.Id}");
            Console.WriteLine();

            HttpOperationResult<FineTuneJobDataResponse> responseJobData = 
                await openAi.FineTuneService
                    .GetAsync(createResponse.Result!.Id, CancellationToken.None)
                        .ConfigureAwait(false);
            
            if (responseJobData.IsSuccess)
            {
                Console.WriteLine(responseJobData.Result!);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(responseJobData);
            }

            Console.WriteLine();
            Console.WriteLine("List fine tune events (sync mode)");
            Console.WriteLine();

            HttpOperationResult<FineTuneJobEventsResponse> eventsResponse = 
                await openAi.FineTuneService
                    .GetEventsAsync(createResponse.Result!.Id, CancellationToken.None)
                        .ConfigureAwait(false);
            
            if (eventsResponse.IsSuccess)
            {
                Console.WriteLine(eventsResponse.Result!);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(eventsResponse);
            }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(async () => { 
                Console.WriteLine();
                Console.WriteLine("List fine tune events (async mode - callback)");
                Console.WriteLine();

                Action<HttpOperationResult<FineTuneJobEvent>> eventResultCallback = 
                    (HttpOperationResult<FineTuneJobEvent> response) =>
                {
                    if (response.IsSuccess)
                    {
                        Console.WriteLine("RESULT (async mode - callback)");
                        Console.WriteLine(response.Result!);
                    }
                    else
                    {
                        Console.WriteLine(response);
                    }
                };

                HttpOperationResult eventsCallbackModeResponse = 
                    await openAi.FineTuneService
                        .GetEventsAsStreamAsync(createResponse.Result!.Id, eventResultCallback, CancellationToken.None)
                            .ConfigureAwait(false);
                
                if (eventsCallbackModeResponse.IsSuccess)
                {
                    Console.WriteLine();
                    Console.WriteLine("DONE (async mode - callback)");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(eventsCallbackModeResponse);
                }
            });

            Task.Run(async () => { 
                Console.WriteLine();
                Console.WriteLine("List fine tune events (async mode - IAsyncEnumerable)");
                Console.WriteLine();

                await foreach (HttpOperationResult<FineTuneJobEvent> response in 
                    openAi.FineTuneService
                        .GetEventsAsStreamAsync(createResponse.Result!.Id, CancellationToken.None))
                {
                    if (response.IsSuccess)
                    {
                        Console.WriteLine("RESULT (async mode - IAsyncEnumerable)");
                        Console.WriteLine(response.Result!);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(response);
                    }
                }
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            Console.WriteLine("Press a key to cancel fine tune job and release async event readers");
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("Cancel fine tune job");
            Console.WriteLine();

            HttpOperationResult<FineTuneCancelResponse> responseCancel = 
                await openAi.FineTuneService
                    .CancelAsync(createResponse.Result!.Id, CancellationToken.None)
                        .ConfigureAwait(false);
            
            if (responseCancel.IsSuccess)
            {
                Console.WriteLine(responseCancel.Result!);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(responseCancel);
            }

        }
        else
        {
            Console.WriteLine(createResponse);
        }

        Console.WriteLine();
        Console.WriteLine("Deleting training file");
        await openAi.FileService.DeleteFileAsync(responseUpload.Result!.Id, CancellationToken.None);

    }
    else
    {
        Console.WriteLine(responseUpload);
    }

}
```


## Files

Files are used to upload documents that can be used with features like Fine-tuning.

More info: https://platform.openai.com/docs/api-reference/files

The following example demonstrates all functions which are related to the file operations.

```c#
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
    uploadRequest.File = new BinaryContentData() 
    { 
        ContentName = "training", 
        SourceStream = File.OpenRead("training.jsonl") 
    };
    uploadRequest.Purpose = "fine-tune";

    HttpOperationResult<FileUploadResponse> responseUpload = 
        await openAi.FileService
            .UploadFileAsync(uploadRequest, CancellationToken.None)
                .ConfigureAwait(false);
    
    if (responseUpload.IsSuccess)
    {
        Console.WriteLine(responseUpload.Result!);
        Console.WriteLine();
        Console.WriteLine("Get file list");
        Console.WriteLine();

        HttpOperationResult<FileListResponse> fileListResult = 
            await openAi.FileService
                .GetFileListAsync(CancellationToken.None)
                    .ConfigureAwait(false);

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

                HttpOperationResult<FileDataResponse> responseFileData = 
                    openAi.FileService
                        .GetFileDataAsync(fileData.Id, CancellationToken.None)
                            .ConfigureAwait(false).GetAwaiter().GetResult();
                
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

                using (FileStream fs = new FileStream(fileData.Id, FileMode.Create, 
                    FileAccess.Write, FileShare.Read))
                {
                    HttpOperationResult<Stream> responseFileDownload = 
                        openAi.FileService
                            .DownloadFileAsync(fileData.Id, fs, CancellationToken.None)
                                .ConfigureAwait(false).GetAwaiter().GetResult();
                    
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

                HttpOperationResult<FileDeleteResponse> responseDelete = 
                    openAi.FileService
                        .DeleteFileAsync(fileData.Id, CancellationToken.None)
                            .ConfigureAwait(false).GetAwaiter().GetResult();
                
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
```


## Moderations

Given a input text, outputs if the model classifies it as violating OpenAI's content policy.
Also it is useful for other services, which can accept these policies.

More info: https://platform.openai.com/docs/api-reference/moderations


```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how to check, if a set of text can pass the OpenAI moderation rules.
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddForgeOpenAI(options => {
            options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
        });
    })
    .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    ModerationRequest request = new ModerationRequest(new string[] { "I want to kill them." });
    HttpOperationResult<ModerationResponse> response = 
        await openAi.ModerationService
            .GetAsync(request, CancellationToken.None)
                .ConfigureAwait(false);

    if (response.IsSuccess)
    {
        Console.WriteLine(response.Result!);
    }
    else
    {
        Console.WriteLine(response);
    }

}
```

## Embeddings

Get a vector representation of a given input that can be easily consumed by machine learning models and algorithms.

More info: https://platform.openai.com/docs/api-reference/embeddings


```c#
static async Task Main(string[] args)
{
    // This example demonstrates, how you can use embedding feature of OpenAI.
    // This feature is useful for search, clustering, recommendations, anomaly detection, etc
    // More information: https://platform.openai.com/docs/guides/embeddings/what-are-embeddings
    //
    // The very first step to create an account at OpenAI: https://platform.openai.com/
    // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
    // Here you can create apiKey(s)

    using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddForgeOpenAI(options => {
            options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
        });
    })
    .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    EmbeddingsRequest request = new EmbeddingsRequest();
    request.InputTextsForEmbeddings.Add("The food was delicious and the waiter...");

    HttpOperationResult<EmbeddingsResponse> response = 
        await openAi.EmbeddingsService
            .GetAsync(request, CancellationToken.None)
                .ConfigureAwait(false);;

    if (response.IsSuccess)
    {
        Console.WriteLine(response.Result!);
    }
    else
    {
        Console.WriteLine(response);
    }

}
```
