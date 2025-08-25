using FlowMediator.Console;
using FlowMediator.Core;

var mediator = new Mediator();

var user = mediator.Send(new GetUserByIdQuery(1));

Console.WriteLine($"User Id: {user.Id}, Name: {user.Name}");
