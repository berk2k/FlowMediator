using FlowMediator.Contracts;
using FlowMediator.Tests.Fixtures;

public class TestEventHandler : IEventHandler<TestEvent>
{
    private readonly HandlerResult _result;

    public TestEventHandler(HandlerResult result)
    {
        _result = result;
    }

    public Task Handle(TestEvent @event, CancellationToken cancellationToken)
    {
        _result.WasHandled = true;
        _result.HandledBy.Add(nameof(TestEventHandler));
        return Task.CompletedTask;
    }
}