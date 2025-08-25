namespace FlowMediator.Contracts
{
    public interface IRequestHandlerAsync<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
