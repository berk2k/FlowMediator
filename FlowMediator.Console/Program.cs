using FlowMediator.Console;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



// configure DI container
var services = new ServiceCollection();

// register FlowMediator and all handlers from current assembly
services.AddFlowMediator(Assembly.GetExecutingAssembly());

// register dependencies for handlers
services.AddSingleton<IUserRepository, UserRepository>();

// build service provider
var provider = services.BuildServiceProvider();

// resolve mediator
var mediator = provider.GetRequiredService<IMediator>();

// test sync handler
var user = mediator.Send(new GetUserByIdQuery(1));
Console.WriteLine($"[SYNC] User Id: {user.Id}, Name: {user.Name}");

// test async handler
var asyncUser = await mediator.SendAsync(new GetUserByIdQuery(2));
Console.WriteLine($"[ASYNC] User Id: {asyncUser.Id}, Name: {asyncUser.Name}");
