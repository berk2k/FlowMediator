using FlowMediator.Console.Events;
using FlowMediator.Contracts;

namespace FlowMediator.Console.EventHandlers
{
    public class TestEventHandler : IRequestHandler<TestEvent, Unit>
    {
        public Task<Unit> Handle(TestEvent request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine($"[Handler] Event triggered with message: {request.Message}");
            return Task.FromResult(Unit.Value);
        }
    }

}
