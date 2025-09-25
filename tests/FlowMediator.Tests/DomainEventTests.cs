using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FlowMediator.Tests.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class DomainEventTests
    {
        [Fact]
        public async Task Should_Handle_DomainEvent_When_EntityCreated()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(DomainEventTests).Assembly);
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var entity = TestEntity.Create("UnitTest");

            // Act
            foreach (var domainEvent in entity.DomainEvents)
            {
                await mediator.SendAsync(domainEvent);
            }

            Assert.Single(entity.DomainEvents);
        }
    }

}
