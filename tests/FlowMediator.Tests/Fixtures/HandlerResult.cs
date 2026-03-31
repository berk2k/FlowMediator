namespace FlowMediator.Tests.Fixtures
{
    public class HandlerResult
    {
        public bool WasHandled { get; set; }
        public List<string> HandledBy { get; set; } = new();
    }
}
