# Forge.OpenAI is a C#.NET client library for OpenAI API, using GPT-4, 3.5 and 3, DALL-E 3, DALL-E 2, Whisper, etc.
OpenAI API client library for .NET. It supports OpenAI and Azure-OpenAI APIs. This library was developed for public usage and it is free to use.
Supported .NET versions:

x >= v4.6.1,

x >= Netstandard 2.0,

x >= dotNetCore 3.1,

.NET 6.0,

.NET 7.0

.NET 8.0

Works with Blazor WebAssembly and Blazor Server.


## Content

 * [Installing](#Installing)
 * [Setup](#Setup)
 * [Installation](#install-from-nuget)
 * [Options](#Options)
 * [Examples](#Examples)
 * [Acquire the available models using OoenAI API](https://github.com/JZO001/Forge.OpenAI/wiki/Acquire-the-available-models-using-OpenAI-API)
 * [Assistants](page under construction)
 * [Chat completion](https://github.com/JZO001/Forge.OpenAI/wiki/Chat-completion)
 * [Batch](page under construction)
 * [Embeddings](https://github.com/JZO001/Forge.OpenAI/wiki/Embeddings)
 * [Image interaction API](https://github.com/JZO001/Forge.OpenAI/wiki/Image-interaction-API)
 * [Files](https://github.com/JZO001/Forge.OpenAI/wiki/Files)
 * [Fine-tune or "Teach" an OpenAI model using the API (Legacy)](https://github.com/JZO001/Forge.OpenAI/wiki/Fine-tune-or-%22Teach%22-an-OpenAI-model-using-the-API-(Legacy))
 * [Fine-tuning Job or "Teach" an OpenAI model using the API](https://github.com/JZO001/Forge.OpenAI/wiki/Fine-tuning-jobs-or-%22Teach%22-an-OpenAI-model-using-the-API)
 * [Messages](page under construction)
 * [Moderations](https://github.com/JZO001/Forge.OpenAI/wiki/Moderations)
 * [Runs](page under construction)
 * [Run Steps](page under construction)
 * [Text completion 1. (Legacy)](https://github.com/JZO001/Forge.OpenAI/wiki/Text-completion-1.-(Legacy))
 * [Text completion 2. (Legacy)](https://github.com/JZO001/Forge.OpenAI/wiki/Text-completion-2.-(Legacy))
 * [Text completion 3. (Legacy)](https://github.com/JZO001/Forge.OpenAI/wiki/Text-completion-3.-(Legacy))
 * [Text edit (Legacy)](https://github.com/JZO001/Forge.OpenAI/wiki/Text-edit-(Legacy))
 * [Threads](page under construction)
 * [Transcription](https://github.com/JZO001/Forge.OpenAI/wiki/Transcription)
 * [Translation](https://github.com/JZO001/Forge.OpenAI/wiki/Translation)
 * [Vector Stores](page under construction)
 * [Vector Store Files](page under construction)
 * [Vector Store File Batches](page under construction)
 * [Azure](#azure)
 * [Configuring HttpClient](https://github.com/JZO001/Forge.OpenAI/wiki/Configuring-HttpClient)


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


## Azure

Setup the service with Azure-OpenAI provider, you need to specify the name of your Azure OpenAI resource as well as your model deployment id.

Prerequisites: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/quickstart?tabs=command-line&pivots=programming-language-studio
Documentation: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference

Example: https://github.com/JZO001/Forge.OpenAI/blob/main/Playgrounds/Azure-OpenAI_Setup_Example/Program.cs
