using FlowMediator.Contracts;
using FlowMediator.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlowMediator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers mediator and all IRequestHandler implementations from the given assembly.
        /// Behaviors (IPipelineBehavior) are NOT automatically registered.
        /// Use this if you want full control over pipeline order.
        /// </summary>
        public static IServiceCollection AddFlowMediator(this IServiceCollection services, Assembly assembly)
        {
            services.RegisterHandlers(assembly);
            services.AddSingleton<IMediator, Mediator>();
            return services;
        }

        /// <summary>
        /// Registers mediator, all IRequestHandler and all IPipelineBehavior implementations from the given assembly.
        /// Behaviors are registered automatically (order is based on reflection).
        /// Use this if you want quick setup without manual ordering.
        /// </summary>
        public static IServiceCollection AddFlowMediatorWithBehaviors(this IServiceCollection services, Assembly assembly)
        {
            services.RegisterHandlers(assembly);
            services.RegisterBehaviors(assembly);
            services.AddSingleton<IMediator, Mediator>();
            return services;
        }

        private static IServiceCollection RegisterHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .Select(i => new { Handler = t, Interface = i }));

            foreach (var h in handlerTypes)
                services.AddTransient(h.Interface, h.Handler);

            return services;
        }

        private static IServiceCollection RegisterBehaviors(this IServiceCollection services, Assembly assembly)
        {
            var behaviorTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))
                    .Select(i => new { Behavior = t, Interface = i }));

            foreach (var b in behaviorTypes)
            {
                if (b.Behavior.IsGenericTypeDefinition)
                    services.AddTransient(b.Interface.GetGenericTypeDefinition(), b.Behavior.GetGenericTypeDefinition());
                else
                    services.AddTransient(b.Interface, b.Behavior);
            }

            return services;
        }
    }

}
