namespace Forge.OpenAI.Interfaces.Infrastructure
{

    /// <summary>Represents the Http Logger service to log network traffic data</summary>
    public interface IApiHttpLoggerService
    {

        /// <summary>Creates a new logger context instance.</summary>
        /// <returns>
        ///   IApiHttpLoggerContext
        /// </returns>
        IApiHttpLoggerContext
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            Create();

    }

}
