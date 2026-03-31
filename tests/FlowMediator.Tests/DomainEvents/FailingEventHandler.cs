using FlowMediator.Contracts;

namespace FlowMediator.Tests.DomainEvents
{
    public class FailingEventHandler : IEventHandler<FailingEvent>
    {
        public Task Handle(FailingEvent @event, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("Handler failed intentionally");
        }
    }
}
