using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FlowMediator.Tests.Fixtures;
using FlowMediator.Tests.Fixtures.Behaviors;
using FlowMediator.Tests.Fixtures.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class SendTests
    {
        private IMediator CreateMediator()
        {
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(SendTests).Assembly);
            return services.BuildServiceProvider().GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task SendAsync_Should_Invoke_Handler()
        {
            var mediator = CreateMediator();

            var result = await mediator.SendAsync(new Ping());

            Assert.Equal("Pong", result);
        }

        [Fact]
        public async Task SendAsync_Should_Throw_When_Request_Is_Null()
        {
            var mediator = CreateMediator();

            await Assert.ThrowsAsync<ArgumentNullException>(
                () => mediator.SendAsync<string>(null!));
        }

        [Fact]
        public async Task SendAsync_Should_Throw_When_No_Handler_Registered()
        {
            var mediator = CreateMediator();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => mediator.SendAsync(new UnregisteredRequest()));
        }
    }
}