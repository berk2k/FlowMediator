namespace FlowMediator.Contracts
{
    /// <summary>
    /// Marker interface for domain events.
    /// Domain events represent something that already happened in the domain.
    /// </summary>
    public interface IDomainEvent : IEvent
    {
    }
}
