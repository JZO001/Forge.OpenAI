using Forge.OpenAI.Infrastructure;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the base class for the services</summary>
    public abstract class ServiceBase
    {

        /// <summary>Initializes a new instance of the <see cref="ServiceBase" /> class.</summary>
        protected ServiceBase()
        {
        }

        /// <summary>Gets the base URI for remote API calls.</summary>
        /// <param name="options">The options.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual string GetBaseUri(OpenAIOptions options)
        {
            return $"{options.BaseAddress}/{options.ApiVersion}/";
        }

    }

}
