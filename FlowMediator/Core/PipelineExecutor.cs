using FlowMediator.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Core
{
    /// <summary>
    /// Executes the pipeline behaviors and eventually the request handler.
    /// </summary>
    public class PipelineExecutor
    {
        private readonly IServiceProvider _provider;

        public PipelineExecutor(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<TResponse> Execute<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> handlerDelegate)
            where TRequest : IRequest<TResponse>
        {
            var behaviors = _provider.GetServices<IPipelineBehavior<TRequest, TResponse>>()
                                     .Reverse()
                                     .ToList();

            RequestHandlerDelegate<TResponse> next = handlerDelegate;

            foreach (var behavior in behaviors)
            {
                var current = next;
                next = () => behavior.Handle(request, cancellationToken, current);
            }

            return await next();
        }
    }
}
