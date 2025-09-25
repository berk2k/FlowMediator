namespace FlowMediator.Contracts
{
    /// <summary>
    /// Marker interface for domain events.
    /// Domain events are treated as IRequest<Unit> so they flow through mediator like commands.
    /// </summary>
    public interface IDomainEvent : IRequest<Unit>
    {
        DateTime OccurredOn { get; }
    }
}

