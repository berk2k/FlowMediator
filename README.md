# FlowMediator

[![NuGet](https://img.shields.io/nuget/v/FlowMediator.svg)](https://www.nuget.org/packages/FlowMediator/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/FlowMediator.svg)](https://www.nuget.org/packages/FlowMediator/)
[![Build](https://github.com/berk2k/FlowMediator/actions/workflows/ci.yml/badge.svg)](https://github.com/berk2k/FlowMediator/actions/workflows/ci.yml)

FlowMediator is a lightweight, opinionated mediator library for .NET 8 and .NET 9.

It focuses on **explicit application flow**, not generic messaging.

---

## Why FlowMediator?

FlowMediator was built around a simple idea:

> **Not everything is a request.**

Commands and queries represent **intent**.  
Events represent **facts that already happened**.

FlowMediator enforces this distinction explicitly.

---

## Core Concepts

| Concept | Description |
|------|-----------|
| `SendAsync` | Commands & Queries (single handler, pipeline-enabled) |
| `PublishAsync` | Events (multiple handlers, side-effect oriented) |
| Pipeline | Applies only to `SendAsync` |
| Events | Never treated as requests |

---

## Installation

```bash
dotnet add package FlowMediator
```

## Request / Response Example
# Define a Request & Handler

``` csharp
public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

public class GetUserByIdQueryHandler 
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        return user ?? throw new Exception("User not found");
    }
}
```

## Register in DI
``` csharp
services.AddFlowMediator(typeof(GetUserByIdQuery).Assembly);
services.AddScoped<IUserRepository, UserRepository>();

services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(LoggingBehavior<,>)
);
```
## Use Mediator
``` csharp
var mediator = provider.GetRequiredService<IMediator>();
var user = await mediator.SendAsync(new GetUserByIdQuery(1));
Console.WriteLine(user.Name);
```
## Events (v2 Model)
Events are published, not sent.

``` csharp
public class UserCreatedEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

await mediator.PublishAsync(new UserCreatedEvent());
```
## Event Handler
``` csharp
public class UserCreatedEventHandler
    : IEventHandler<UserCreatedEvent>
{
    public Task Handle(
        UserCreatedEvent @event,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("User created");
        return Task.CompletedTask;
    }
}
```

## Pipelines
Pipelines apply only to SendAsync.

They are ideal for:

- Logging
- Validation
- Transactions

Events are executed outside the pipeline.

## When to Use FlowMediator
- Clean application flow
- Explicit CQRS
- Domain-driven design
- Predictable execution

## Roadmap
- FlowContext (CorrelationId, UserId, Metadata)
- Step-based execution model
- Observability and retry 

## ⚠️ Disclaimer

FlowMediator focuses on **application flow and execution semantics**.

It does **not** provide:
- Authorization or authentication
- Security policies
- Exception handling strategies
- Infrastructure-level concerns

These responsibilities intentionally remain in the application layer.

FlowMediator is designed to be **explicit and predictable**,  
not a full application framework.

## 🤝 Contributing
Contributions, bug reports, and feature requests are welcome.

## 📜 License
Licensed under the MIT License.



