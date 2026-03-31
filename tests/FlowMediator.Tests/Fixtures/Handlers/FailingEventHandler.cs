using FlowMediator.Contracts;
using FlowMediator.Tests.Fixtures.Events;

namespace FlowMediator.Tests.Fixtures.Handlers
{
    public class FailingEventHandler : IEventHandler<FailingEvent>
    {
        public Task Handle(FailingEvent @event, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("Handler failed intentionally");
        }
    }
}
