using FlowMediator.Contracts;

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
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
                throw new InvalidOperationException($"No handler found for {request.GetType().Name}");

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException($"Handler {handler.GetType().Name} does not contain Handle method");

            RequestHandlerDelegate<TResponse> handlerDelegate = async () =>
            {
                var result = method.Invoke(handler, new object[] { request, cancellationToken });
                if (result is Task<TResponse> task)
                    return await task;

                throw new InvalidOperationException(
                    $"Handler {handler.GetType().Name} did not return Task<{typeof(TResponse).Name}>");
            };

            return await _pipelineExecutor.Execute((dynamic)request, cancellationToken, handlerDelegate);
        }
    }
}
