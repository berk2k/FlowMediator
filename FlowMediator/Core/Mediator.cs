using FlowMediator.Contracts;

namespace FlowMediator.Core
{
    public class Mediator : IMediator
    {

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            
            var requestType = request.GetType();
            var responseType = typeof(TResponse);

            // this part of code builds type of IRequestHandler<Request,Response>
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(requestType, responseType);

            // with this part we aim to find the class that implements this handler type
            var handler = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t =>
                    handlerType.IsAssignableFrom(t) &&
                    !t.IsInterface && !t.IsAbstract);

            if(handler == null)
                throw new InvalidOperationException($"No handler found for {requestType.Name}");

            // creating an instance of the handler
            var handlerInstance = Activator.CreateInstance(handler);

            // executing the handle method of the handler
            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException($"Handler {handler.Name} does not implement Handle method");

            return (TResponse)method.Invoke(handlerInstance, new object[] { request });
        }
    }
}
