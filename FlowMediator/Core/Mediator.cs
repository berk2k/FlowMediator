using FlowMediator.Contracts;

namespace FlowMediator.Core
{
    /// <summary>
    /// The core of FlowMediator.
    /// Supports both sync and async request handling.
    /// </summary>
    public class Mediator : IMediator
    {
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

            var handler = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => handlerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (handler == null)
                throw new InvalidOperationException($"No handler found for {requestType.Name}");

            var handlerInstance = Activator.CreateInstance(handler);
            var method = handlerType.GetMethod("Handle");

            return (TResponse)method.Invoke(handlerInstance, new object[] { request });
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandlerAsync<,>).MakeGenericType(requestType, typeof(TResponse));

            var handler = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => handlerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (handler == null)
                throw new InvalidOperationException($"No async handler found for {requestType.Name}");

            var handlerInstance = Activator.CreateInstance(handler);
            var method = handlerType.GetMethod("HandleAsync");

            var result = method.Invoke(handlerInstance, new object[] { request, cancellationToken });

            if (result is Task<TResponse> task)
            {
                return await task;
            }

            throw new InvalidOperationException($"Handler {handler.Name} did not return Task<{typeof(TResponse).Name}>");
        }
    }
}


