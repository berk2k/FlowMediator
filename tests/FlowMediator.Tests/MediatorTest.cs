using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{

    public class MediatorTests
    {
        [Fact]
        public async Task SendAsync_Should_Invoke_Handler()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(MediatorTests).Assembly);
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.SendAsync(new Ping());

            // Assert
            result.Should().Be("Pong");
        }

        [Fact]
        public async Task SendAsync_Should_Throw_When_Request_Is_Null()
        {
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(MediatorTests).Assembly);
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.SendAsync<string>(null!));
        }
    }

    // Test Request & Handler
    public class Ping : IRequest<string> { }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
            => Task.FromResult("Pong");
    }

}
