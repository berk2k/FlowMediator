using FlowMediator.Contracts;

public class TestEventHandler : IEventHandler<TestEvent>
{
    public static bool WasHandled { get; private set; }

    public Task Handle(TestEvent @event, CancellationToken cancellationToken)
    {
        WasHandled = true;
        return Task.CompletedTask;
    }
}
