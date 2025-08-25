using FlowMediator.Contracts;
using FlowMediator.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlowMediator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlowMediator(this IServiceCollection services, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .Select(i => new { Handler = t, Interface = i }));

            foreach (var h in handlerTypes)
                services.AddTransient(h.Interface, h.Handler);

            var behaviorTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))
                    .Select(i => new { Behavior = t, Interface = i }));

            foreach (var b in behaviorTypes)
                services.AddTransient(b.Interface, b.Behavior);

            services.AddSingleton<IMediator, Mediator>();

            return services;
        }
    }
}
