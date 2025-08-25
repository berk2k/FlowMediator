namespace FlowMediator.Contracts
{
    /// <summary>
    /// Represents the next element in the pipeline. 
    /// Calling this will either trigger the next behavior or the final request handler.
    /// </summary>
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
}
