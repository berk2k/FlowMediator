using FlowMediator.Console.Behaviors;
using FlowMediator.Console.Events;
using FlowMediator.Console.EventHandlers;
using FlowMediator.Console.Queries;
using FlowMediator.Console.Repositories;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

// ---------------------------------
// Service registration
// ---------------------------------

var services = new ServiceCollection();

// Register FlowMediator (handlers auto, pipeline manual)
services.AddFlowMediator(Assembly.GetExecutingAssembly());

// App services
services.AddSingleton<IUserRepository, UserRepository>();

// FluentValidation validators
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Pipeline behaviors (explicit order)
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

// ---------------------------------
// Query test (SendAsync)
// ---------------------------------

Console.WriteLine("---- QUERY TEST ----");

var user = await mediator.SendAsync(new GetUserByIdQuery(1));
Console.WriteLine($"User Id: {user.Id}, Name: {user.Name}");

try
{
    await mediator.SendAsync(new GetUserByIdQuery(0));
}
catch (ValidationException ex)
{
    Console.WriteLine("Validation failed:");
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($" - {error.PropertyName}: {error.ErrorMessage}");
    }
}

// ---------------------------------
// Event test (PublishAsync)
// ---------------------------------

Console.WriteLine();
Console.WriteLine("---- EVENT TEST ----");

await mediator.PublishAsync(
    new UserCreatedEvent(user.Name));

Console.WriteLine();
Console.WriteLine("Demo finished successfully.");
