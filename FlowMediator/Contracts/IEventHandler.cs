namespace FlowMediator.Contracts
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
