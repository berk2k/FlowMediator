namespace FlowMediator.Contracts
{
    /// <summary>
    /// Represents a pipeline behavior that can wrap request handling with additional logic 
    /// (e.g. logging, validation, transactions, caching).
    /// </summary>
    public interface IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next);
    }
}
