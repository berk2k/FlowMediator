namespace FlowMediator.Contracts
{
    /// <summary>
    /// A handler that processes a request.
    /// </summary>

    public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
