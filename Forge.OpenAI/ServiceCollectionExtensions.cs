using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Services;
using Forge.OpenAI.Services.Endpoints;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forge.OpenAI
{

    /// <summary>ServiceCollection extension methods</summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Registers the Forge OpenAPI services as singletons
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeOpenAI(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsSingleton(services)
                .AddSingleton<IProviderEndpointService, OpenAIProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.OpenAI;
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge OpenAPI services as scoped
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeOpenAIAsScoped(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsScoped(services)
                .AddScoped<IProviderEndpointService, OpenAIProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.OpenAI;
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge OpenAPI services as transient
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeOpenAIAsTransient(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsTransient(services)
                .AddTransient<IProviderEndpointService, OpenAIProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.OpenAI;
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge Azure OpenAPI services as singletons
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeAzureOpenAI(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsSingleton(services)
                .AddSingleton<IProviderEndpointService, AzureProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.Azure;
                    configureOptions.BaseAddress = string.Format(OpenAIDefaultOptions.DefaultAzureBaseAddress, OpenAIDefaultOptions.DefaultAzureResourceName);
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge Azure OpenAPI services as scoped
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeAzureOpenAIAsScoped(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsScoped(services)
                .AddScoped<IProviderEndpointService, AzureProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.Azure;
                    configureOptions.BaseAddress = string.Format(OpenAIDefaultOptions.DefaultAzureBaseAddress, OpenAIDefaultOptions.DefaultAzureResourceName);
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge Azure OpenAPI services as transient
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeAzureOpenAIAsTransient(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure = null)
        {
            return AddServicesAsTransient(services)
                .AddTransient<IProviderEndpointService, AzureProviderEndpointService>()
                .AddTransient<IApiHttpService, ApiHttpService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.Azure;
                    configureOptions.BaseAddress = string.Format(OpenAIDefaultOptions.DefaultAzureBaseAddress, OpenAIDefaultOptions.DefaultAzureResourceName);
                    configure?.Invoke(configureOptions);
                });
        }

        private static IServiceCollection AddServicesAsSingleton(IServiceCollection services)
        {
            services.AddHttpClient<IApiHttpService>(Consts.HTTP_CLIENT_FACTORY_NAME);

            return services
                .AddSingleton<IApiHttpLoggerService, ApiHttpLoggerService>()
                .AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>()
                .AddSingleton<IModelService, ModelService>()
                .AddSingleton<ITextCompletionService, TextCompletionService>()
                .AddSingleton<ITextEditService, TextEditService>()
                .AddSingleton<IModerationService, ModerationService>()
                .AddSingleton<IEmbeddingsService, EmbeddingsService>()
                .AddSingleton<IImageService, ImageService>()
                .AddSingleton<IFileService, FileService>()
                .AddSingleton<IFineTuneService, FineTuneService>()
                .AddSingleton<IFineTuningJobService, FineTuningJobService>()
                .AddSingleton<ISpeechService, SpeechService>()
                .AddSingleton<ITranscriptionService, TranscriptionService>()
                .AddSingleton<ITranslationService, TranslationService>()
                .AddSingleton<IChatCompletionService, ChatCompletionService>()
                .AddSingleton<IAssistantService, AssistantService>()
                .AddSingleton<IAssistantFileService, AssistantFileService>()
                .AddSingleton<IOpenAIService, OpenAIService>()
                .AddSingleton<IThreadsService, ThreadsService>()
                .AddSingleton<IMessageService, MessageService>()
                .AddSingleton<IMessageFileService, MessageFileService>()
                .AddSingleton<IRunService, RunService>()
                .AddSingleton<IRunStepService, RunStepService>();
        }

        private static IServiceCollection AddServicesAsScoped(IServiceCollection services)
        {
            services.AddHttpClient<IApiHttpService>(Consts.HTTP_CLIENT_FACTORY_NAME);

            return services
                .AddScoped<IApiHttpLoggerService, ApiHttpLoggerService>()
                .AddScoped<IApiHttpClientFactory, ApiHttpClientFactory>()
                .AddScoped<IModelService, ModelService>()
                .AddScoped<ITextCompletionService, TextCompletionService>()
                .AddScoped<ITextEditService, TextEditService>()
                .AddScoped<IModerationService, ModerationService>()
                .AddScoped<IEmbeddingsService, EmbeddingsService>()
                .AddScoped<IImageService, ImageService>()
                .AddScoped<IFileService, FileService>()
                .AddScoped<IFineTuneService, FineTuneService>()
                .AddScoped<IFineTuningJobService, FineTuningJobService>()
                .AddScoped<ISpeechService, SpeechService>()
                .AddScoped<ITranscriptionService, TranscriptionService>()
                .AddScoped<ITranslationService, TranslationService>()
                .AddScoped<IChatCompletionService, ChatCompletionService>()
                .AddScoped<IAssistantService, AssistantService>()
                .AddScoped<IAssistantFileService, AssistantFileService>()
                .AddScoped<IOpenAIService, OpenAIService>()
                .AddScoped<IThreadsService, ThreadsService>()
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IMessageFileService, MessageFileService>()
                .AddScoped<IRunService, RunService>()
                .AddScoped<IRunStepService, RunStepService>();
        }

        private static IServiceCollection AddServicesAsTransient(IServiceCollection services)
        {
            services.AddHttpClient<IApiHttpService>(Consts.HTTP_CLIENT_FACTORY_NAME);

            return services
                .AddScoped<IApiHttpLoggerService, ApiHttpLoggerService>()
                .AddTransient<IApiHttpClientFactory, ApiHttpClientFactory>()
                .AddTransient<IModelService, ModelService>()
                .AddTransient<ITextCompletionService, TextCompletionService>()
                .AddTransient<ITextEditService, TextEditService>()
                .AddTransient<IModerationService, ModerationService>()
                .AddTransient<IEmbeddingsService, EmbeddingsService>()
                .AddTransient<IImageService, ImageService>()
                .AddTransient<IFileService, FileService>()
                .AddTransient<IFineTuneService, FineTuneService>()
                .AddTransient<IFineTuningJobService, FineTuningJobService>()
                .AddTransient<ISpeechService, SpeechService>()
                .AddTransient<ITranscriptionService, TranscriptionService>()
                .AddTransient<ITranslationService, TranslationService>()
                .AddTransient<IChatCompletionService, ChatCompletionService>()
                .AddTransient<IAssistantService, AssistantService>()
                .AddTransient<IAssistantFileService, AssistantFileService>()
                .AddTransient<IOpenAIService, OpenAIService>()
                .AddTransient<IThreadsService, ThreadsService>()
                .AddTransient<IMessageService, MessageService>()
                .AddTransient<IMessageFileService, MessageFileService>()
                .AddTransient<IRunService, RunService>()
                .AddTransient<IRunStepService, RunStepService>();
        }

    }

}