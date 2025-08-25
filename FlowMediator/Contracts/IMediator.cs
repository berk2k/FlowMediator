namespace FlowMediator.Contracts
{
    /// <summary>
    /// The mediator that receives a request and dispatches it to the appropriate handler.
    /// </summary>

    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
