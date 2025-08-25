using FlowMediator.Contracts;

namespace FlowMediator.Console.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            System.Console.WriteLine($"[LOG] Starting {typeof(TRequest).Name}");

            var response = await next(); 

            System.Console.WriteLine($"[LOG] Finished {typeof(TResponse).Name}");
            return response;
        }
    }
}
