using FlowMediator.Contracts;

public class TestEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public string Message { get; }

    public TestEvent(string message)
    {
        Message = message;
    }
}
