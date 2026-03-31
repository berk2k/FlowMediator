using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FlowMediator.Tests.Fixtures;
using FlowMediator.Tests.Fixtures.Events;
using FlowMediator.Tests.Fixtures.Handlers;
using FlowMediator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator.Tests
{
    public class PublishTests
    {
        private (IMediator mediator, HandlerResult result) CreateMediator()
        {
            var result = new HandlerResult();
            var services = new ServiceCollection();
            services.AddSingleton(result);
            services.AddFlowMediator(typeof(PublishTests).Assembly);
            var provider = services.BuildServiceProvider();
            return (provider.GetRequiredService<IMediator>(), result);
        }

        [Fact]
        public async Task PublishAsync_Should_Invoke_EventHandlers()
        {
            var (mediator, result) = CreateMediator();

            await mediator.PublishAsync(new TestEvent("hello"));

            Assert.True(result.WasHandled);
        }

        [Fact]
        public async Task PublishAsync_Should_Stop_On_First_Handler_Exception()
        {
            var (mediator, _) = CreateMediator();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => mediator.PublishAsync(new FailingEvent()));
        }

        [Fact]
        public async Task PublishAsync_Should_Not_Invoke_Remaining_Handlers_After_Exception()
        {
            var result = new HandlerResult();
            var services = new ServiceCollection();
            services.AddSingleton(result);
            services.AddSingleton<IMediator, Mediator>();
            services.AddTransient<IEventHandler<FailingEvent>, FailingEventHandler>();
            services.AddTransient<IEventHandler<FailingEvent>, AfterFailingEventHandler>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            try { await mediator.PublishAsync(new FailingEvent()); } catch { }

            Assert.DoesNotContain("AfterFailingHandler", result.HandledBy);
        }
    }
}