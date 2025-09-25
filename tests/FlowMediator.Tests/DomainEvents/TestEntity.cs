using FlowMediator.Contracts;

namespace FlowMediator.Tests.DomainEvents
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; private set; }

        public static TestEntity Create(string name)
        {
            var entity = new TestEntity { Name = name };
            entity.AddDomainEvent(new TestEvent($"Entity {name} created"));
            return entity;
        }
    }

}
