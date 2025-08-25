using FlowMediator.Console;
using FlowMediator.Core;

var mediator = new Mediator();

var user = mediator.Send(new GetUserByIdQuery(1));
Console.WriteLine($"[SYNC] User Id: {user.Id}, Name: {user.Name}");

// Async handler usage
var asyncUser = await mediator.SendAsync(new GetUserByIdQuery(2));
Console.WriteLine($"[ASYNC] User Id: {asyncUser.Id}, Name: {asyncUser.Name}");
