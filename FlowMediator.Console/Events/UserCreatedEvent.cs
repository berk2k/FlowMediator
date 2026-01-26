using FlowMediator.Contracts;

namespace FlowMediator.Console.Events
{
    public class UserCreatedEvent : IEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public string UserName { get; }

        public UserCreatedEvent(string userName)
        {
            UserName = userName
                ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
