using Forge.OpenAI.Interfaces.Infrastructure;

namespace Forge.OpenAI.Infrastructure
{

    internal class AsyncEventInfo<TResult> : IAsyncEventInfo<TResult> where TResult : class
    {

        public AsyncEventInfo(string @event, TResult result)
        {
            Event = @event;
            Data = result;
        }

        public string Event { get; }

        public TResult Data { get; }

    }

}
