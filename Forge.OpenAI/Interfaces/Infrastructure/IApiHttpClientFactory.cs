namespace Forge.OpenAI.Interfaces.Infrastructure
{

    /// <summary>Represents a HttpClient container which used for unique HttpClient configuration</summary>
    public interface IApiHttpClientFactory
    {

        /// <summary>Gets the HTTP client.</summary>
        /// <value>The HTTP client.</value>
        System.Net.Http.HttpClient GetHttpClient();

    }

}
