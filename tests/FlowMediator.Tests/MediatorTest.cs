using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class MediatorTests
    {
        [Fact]
        public async Task SendAsync_Should_Invoke_Handler()
        {
            var services = new ServiceCollection();
            services.AddFlowMediator(typeof(MediatorTests).Assembly);
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var result = await mediator.SendAsync(new Ping());

            Assert.Equal("Pong", result);
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
