using FlowMediator.Contracts;
using FlowMediator.Tests.Fixtures.Requests;

namespace FlowMediator.Tests.Fixtures.Behaviors
{
    public class FirstBehavior : IPipelineBehavior<Ping, string>
    {
        private readonly HandlerResult _result;

        public FirstBehavior(HandlerResult result) => _result = result;

        public async Task<string> Handle(
            Ping request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<string> next)
        {
            _result.HandledBy.Add("First");
            return await next();
        }
    }
}