using FlowMediator.Contracts;
using FlowMediator.Tests.Fixtures.Requests;

namespace FlowMediator.Tests.Fixtures.Handlers
{
    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
            => Task.FromResult("Pong");
    }
}