namespace FlowMediator.Contracts
{
    public interface IEvent
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
