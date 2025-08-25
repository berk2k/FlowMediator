using FlowMediator.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowMediator.Core
{
    /// <summary>
    /// FlowMediator with DI support.
    /// Handlers are resolved via IServiceProvider
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
                throw new InvalidOperationException($"No handler found for {request.GetType().Name}");

            var method = handlerType.GetMethod("Handle");
            return (TResponse)method.Invoke(handler, new object[] { request });
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(IRequestHandlerAsync<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
                throw new InvalidOperationException($"No async handler found for {request.GetType().Name}");

            var method = handlerType.GetMethod("HandleAsync");
            var result = method.Invoke(handler, new object[] { request, cancellationToken });

            if (result is Task<TResponse> task)
                return await task;

            throw new InvalidOperationException($"Handler {handler.GetType().Name} did not return Task<{typeof(TResponse).Name}>");
        }
    }
}
