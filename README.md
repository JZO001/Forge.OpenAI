# Forge.OpenAI
OpenAI API client library for .NET. This is not an official library, I was developed it for myself, for public and it is free to use.
Supported .NET versions:
x >= v4.6.1
x >= Netstandard 2.0
x >= dotNetCore 3.1
.NET 6.0
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


## Example - Text completition 1.

The next code demonstrates how to give a simple instruction (prompt). The whole answer generated on the OpenAI side remotelly,
than the answer will send in a response.

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

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    // in this scenario the answer generated on server side, than the whole text will be sent in one pass
    // this method is useful for small conversatons and for short answers

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Say this is a test";

    HttpOperationResult<TextCompletionResponse> response = await openAi.TextCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
    if (response.IsSuccess)
    {
        response.Result!.Completions.ForEach(c => Console.WriteLine(c.Text));

        request.Prompt = "Are you sure?";
        response = await openAi.TextCompletionService.GetAsync(request, CancellationToken.None).ConfigureAwait(false);
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


## Example - Text completition 2.

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
                options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
            });
        })
        .Build();

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    // this method is useful for older .NET where the IAsyncEnumerable is not supported,
    // or you just simply does not prefer this way

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Write a C# code which demonstrate how to open a text file and read its content";
    request.MaxTokens = 4096 - request.Prompt.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
    request.Temperature = 0.1; // lower value means more precise answer

    Console.WriteLine(request.Prompt);

    Action<HttpOperationResult<TextCompletionResponse>> receivedDataHandler = (HttpOperationResult<TextCompletionResponse> response) => 
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

    HttpOperationResult response = await openAi.TextCompletionService.GetStreamAsync(request, receivedDataHandler, CancellationToken.None).ConfigureAwait(false);
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


## Example - Text completition 3.

The last example in this topic demonstrates, how you can receive an answer in streamed mode also.

This version works with IAsyncEnumerable. It is not supported in older .NET versions.

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

    IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

    TextCompletionRequest request = new TextCompletionRequest();
    request.Prompt = "Write a C# code which demonstrate how to write some text into file";
    request.MaxTokens = 4096 - request.Prompt.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length; // calculating max token
    request.Temperature = 0.1; // lower value means more precise answer

    Console.WriteLine(request.Prompt);

    await foreach (HttpOperationResult<TextCompletionResponse> response in openAi.TextCompletionService.GetStreamAsync(request, CancellationToken.None))
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
```
