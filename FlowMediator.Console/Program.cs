using FlowMediator.Console;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



var services = new ServiceCollection();

// register mediator + handlers + behaviors automatically
services.AddFlowMediator(Assembly.GetExecutingAssembly());
services.AddSingleton<IUserRepository, UserRepository>();

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

// test: request via mediator
var user = await mediator.SendAsync(new GetUserByIdQuery(1));

Console.WriteLine($"User Id: {user.Id}, Name: {user.Name}");
