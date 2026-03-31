using FlowMediator.Contracts;
using FlowMediator.Tests;

public class AfterFailingEventHandler : IEventHandler<FailingEvent>
{
    private readonly HandlerResult _result;

    public AfterFailingEventHandler(HandlerResult result)
    {
        _result = result;
    }

    public Task Handle(FailingEvent @event, CancellationToken cancellationToken)
    {
        _result.HandledBy.Add("AfterFailingHandler");
        return Task.CompletedTask;
    }
}