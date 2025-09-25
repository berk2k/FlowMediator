namespace FlowMediator.Contracts
{
    /// <summary>
    /// Represents a void result (like MediatR.Unit).
    /// </summary>
    public struct Unit
    {
        public static readonly Unit Value = new Unit();
    }
}

