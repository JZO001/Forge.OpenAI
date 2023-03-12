using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Services;
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
                .AddSingleton<ITranscriptionService, TranscriptionService>()
                .AddSingleton<ITranslationService, TranslationService>()
                .AddSingleton<IOpenAIService, OpenAIService>()
                .Configure<OpenAIOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                });
        }

    }

}