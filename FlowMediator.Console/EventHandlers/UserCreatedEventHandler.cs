using FlowMediator.Contracts;
using FlowMediator.Console.Events;

namespace FlowMediator.Console.EventHandlers
{
    public class UserCreatedEventHandler
        : IEventHandler<UserCreatedEvent>
    {
        public Task Handle(
            UserCreatedEvent @event,
            CancellationToken cancellationToken)
        {
            System.Console.WriteLine(
                $"[EVENT] User created: {@event.UserName}");

            return Task.CompletedTask;
        }
    }
}
