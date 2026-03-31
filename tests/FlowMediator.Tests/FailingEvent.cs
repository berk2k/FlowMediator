using FlowMediator.Contracts;

namespace FlowMediator.Tests
{
    public class FailingEvent : IEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
