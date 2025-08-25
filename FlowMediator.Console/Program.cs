using FlowMediator.Console;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;



var services = new ServiceCollection();


services.AddFlowMediator(Assembly.GetExecutingAssembly());

services.AddSingleton<IUserRepository, UserRepository>();

services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();


var user = await mediator.SendAsync(new GetUserByIdQuery(1));
Console.WriteLine($"User Id: {user.Id}, Name: {user.Name}");


try
{
    var invalidUser = await mediator.SendAsync(new GetUserByIdQuery(0));
    Console.WriteLine($"User Id: {invalidUser.Id}, Name: {invalidUser.Name}");
}
catch (ValidationException ex)
{
    Console.WriteLine("Validation failed:");
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($"   - {error.PropertyName}: {error.ErrorMessage}");
    }
}
