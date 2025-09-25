using FlowMediator.Contracts;

namespace FlowMediator.Console.Events
{
    public class TestEvent : IDomainEvent
    {
        public string Message { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public TestEvent(string message) => Message = message;
    }

}
