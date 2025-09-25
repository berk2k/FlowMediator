using FlowMediator.Console;
using FlowMediator.Console.Behaviors;
using FlowMediator.Console.Entities;
using FlowMediator.Console.Queries;
using FlowMediator.Console.Repositories;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

// handlers + behavior pipelines are automatic
//var services = new ServiceCollection();
//services.AddFlowMediatorWithBehaviors(Assembly.GetExecutingAssembly());
//services.AddSingleton<IUserRepository, UserRepository>();
//services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//var provider = services.BuildServiceProvider();
//var mediator = provider.GetRequiredService<IMediator>();

// handlers are automatic, behavior pipelines manual 
var services = new ServiceCollection();

services.AddFlowMediator(Assembly.GetExecutingAssembly());

services.AddSingleton<IUserRepository, UserRepository>();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

// ------------------------
// Query test
// ------------------------
var user = await mediator.SendAsync(new GetUserByIdQuery(1));
System.Console.WriteLine($"User Id: {user.Id}, Name: {user.Name}");

try
{
    var invalidUser = await mediator.SendAsync(new GetUserByIdQuery(0));
    System.Console.WriteLine($"User Id: {invalidUser.Id}, Name: {invalidUser.Name}");
}
catch (ValidationException ex)
{
    System.Console.WriteLine("Validation failed:");
    foreach (var error in ex.Errors)
    {
        System.Console.WriteLine($"   - {error.PropertyName}: {error.ErrorMessage}");
    }
}

// ------------------------
// Domain Event test
// ------------------------
var testEntity = TestEntity.Create("SampleEntity");


foreach (var domainEvent in testEntity.DomainEvents)
{
    await mediator.SendAsync(domainEvent);
}
testEntity.ClearDomainEvents();
