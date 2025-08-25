using FlowMediator.Console;
using FlowMediator.Contracts;
using FlowMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



var services = new ServiceCollection();
services.AddFlowMediator(Assembly.GetExecutingAssembly());
services.AddSingleton<IUserRepository, UserRepository>();

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

var user = await mediator.SendAsync(new GetUserByIdQuery(1));
Console.WriteLine($"[ASYNC] User Id: {user.Id}, Name: {user.Name}");
