using FlowMediator.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlowMediator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlowMediator(this IServiceCollection services, Assembly assembly)
        {
            // Register all IRequestHandler<TRequest, TResponse>
            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .Select(i => new { Handler = t, Interface = i }));

            foreach (var h in handlerTypes)
            {
                services.AddTransient(h.Interface, h.Handler);
            }

            // Register all IRequestHandlerAsync<TRequest, TResponse>
            var asyncHandlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandlerAsync<,>))
                    .Select(i => new { Handler = t, Interface = i }));

            foreach (var h in asyncHandlerTypes)
            {
                services.AddTransient(h.Interface, h.Handler);
            }

            // Register mediator itself
            services.AddSingleton<IMediator, Core.Mediator>();

            return services;
        }
    }
}
