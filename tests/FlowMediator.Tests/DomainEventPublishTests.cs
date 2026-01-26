using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class DomainEventPublishTests
    {
        [Fact]
        public async Task PublishAsync_Should_Invoke_EventHandlers()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(DomainEventPublishTests).Assembly);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var testEvent = new TestEvent("hello");

            // Act
            await mediator.PublishAsync(testEvent);

            // Assert
            Assert.True(TestEventHandler.WasHandled);
        }
    }

}
