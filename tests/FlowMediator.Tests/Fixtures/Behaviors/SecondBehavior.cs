using FlowMediator.Contracts;
using FlowMediator.Tests.Fixtures.Requests;

namespace FlowMediator.Tests.Fixtures.Behaviors
{
    public class SecondBehavior : IPipelineBehavior<Ping, string>
    {
        private readonly HandlerResult _result;

        public SecondBehavior(HandlerResult result) => _result = result;

        public async Task<string> Handle(
            Ping request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<string> next)
        {
            _result.HandledBy.Add("Second");
            return await next();
        }
    }
}