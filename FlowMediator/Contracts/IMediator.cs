namespace FlowMediator.Contracts
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);

        Task PublishAsync<TEvent>(
            TEvent @event,
            CancellationToken cancellationToken = default)
            where TEvent : IEvent;
    }
}
