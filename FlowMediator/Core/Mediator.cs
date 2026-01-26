using FlowMediator.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Core
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PipelineExecutor _pipelineExecutor;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _pipelineExecutor = new PipelineExecutor(serviceProvider);
        }

        public async Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // Resolve handler type
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);
            if (handler is null)
                throw new InvalidOperationException(
                    $"No handler found for request type {request.GetType().Name}");

            var method = handlerType.GetMethod("Handle");
            if (method is null)
                throw new InvalidOperationException(
                    $"Handler {handler.GetType().Name} does not contain Handle method");

            RequestHandlerDelegate<TResponse> handlerDelegate = async () =>
            {
                var result = method.Invoke(handler, new object[] { request, cancellationToken });

                if (result is Task<TResponse> task)
                    return await task;

                throw new InvalidOperationException(
                    $"Handler {handler.GetType().Name} did not return Task<{typeof(TResponse).Name}>");
            };

            // Pipeline only applies to Send (Command / Query)
            return await _pipelineExecutor.Execute(
                (dynamic)request,
                cancellationToken,
                handlerDelegate);
        }


        public async Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));

            var handlers = _serviceProvider
                .GetServices<IEventHandler<TEvent>>()
                .ToList();

            foreach (var handler in handlers)
            {
                await handler.Handle(@event, cancellationToken);
            }
        }
    }
}
