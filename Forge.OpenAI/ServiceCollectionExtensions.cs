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
        /// Registers the Forge OpenAPI services
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeOpenAI(this IServiceCollection services)
            => services.AddForgeOpenAI(null);

        /// <summary>
        /// Registers the Forge OpenAPI services
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeOpenAI(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure)
        {
            return AddServices(services)
                .AddSingleton<IProviderEndpointService, OpenAIProviderEndpointService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.OpenAI;
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge Azure OpenAPI services
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeAzureOpenAI(this IServiceCollection services)
            => services.AddForgeAzureOpenAI(null);

        /// <summary>
        /// Registers the Forge Azure OpenAPI services
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeAzureOpenAI(this IServiceCollection services, Action<OpenAIOptions>
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            configure)
        {
            return AddServices(services)
                .AddSingleton<IProviderEndpointService, AzureProviderEndpointService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configureOptions.AIServiceProviderType = AIServiceProviderTypeEnum.Azure;
                    configureOptions.BaseAddress = string.Format(OpenAIDefaultOptions.DefaultAzureBaseAddress, OpenAIDefaultOptions.DefaultAzureResourceName);
                    configure?.Invoke(configureOptions);
                });
        }

        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddHttpClient<IApiHttpService>(Consts.HTTP_CLIENT_FACTORY_NAME);

            return services
                .AddSingleton<IApiHttpLoggerService, ApiHttpLoggerService>()
                .AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>()
                .AddSingleton<IApiHttpService, ApiHttpService>()
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
                .AddSingleton<IThreadsService, ThreadsService>();
        }

    }

}