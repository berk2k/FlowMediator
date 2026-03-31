using FlowMediator.Contracts;

namespace FlowMediator.Tests.Fixtures.Behaviors
{
    public class TrackingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly HandlerResult _result;

        public TrackingBehavior(HandlerResult result)
        {
            _result = result;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _result.HandledBy.Add(nameof(TrackingBehavior<TRequest, TResponse>));
            return await next();
        }
    }
}