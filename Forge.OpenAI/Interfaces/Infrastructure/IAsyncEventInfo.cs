namespace Forge.OpenAI.Interfaces.Infrastructure
{

    /// <summary>
    /// Represents an asynchronous event information.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IAsyncEventInfo<out TResult> where TResult : class
    {

        /// <summary>
        /// Gets the event type.
        /// </summary>
        string Event { get; }

        /// <summary>
        /// Gets the event object.
        /// </summary>
        TResult Data { get; }

    }

}
