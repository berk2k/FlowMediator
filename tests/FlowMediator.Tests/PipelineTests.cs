using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FlowMediator.Tests.Fixtures;
using FlowMediator.Tests.Fixtures.Behaviors;
using FlowMediator.Tests.Fixtures.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class PipelineTests
    {
        [Fact]
        public async Task SendAsync_Should_Execute_Pipeline_Behavior()
        {
            var result = new HandlerResult();
            var services = new ServiceCollection();
            services.AddSingleton(result);
            services.AddFlowMediator(typeof(PipelineTests).Assembly);
            services.AddTransient<IPipelineBehavior<Ping, string>, TrackingBehavior<Ping, string>>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            await mediator.SendAsync(new Ping());

            Assert.Contains(nameof(TrackingBehavior<Ping, string>), result.HandledBy);
        }

        [Fact]
        public async Task SendAsync_Should_Execute_Pipeline_Behaviors_In_Order()
        {
            var result = new HandlerResult();
            var services = new ServiceCollection();
            services.AddSingleton(result);
            services.AddFlowMediator(typeof(PipelineTests).Assembly);
            services.AddTransient<IPipelineBehavior<Ping, string>, FirstBehavior>();
            services.AddTransient<IPipelineBehavior<Ping, string>, SecondBehavior>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            await mediator.SendAsync(new Ping());

            Assert.Equal(new[] { "First", "Second" }, result.HandledBy);
        }
    }
}