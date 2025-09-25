# FlowMediator
[![NuGet](https://img.shields.io/nuget/v/FlowMediator.svg)](https://www.nuget.org/packages/FlowMediator/) [![NuGet Downloads](https://img.shields.io/nuget/dt/FlowMediator.svg)](https://www.nuget.org/packages/FlowMediator/) [![Build](https://github.com/berk2k/FlowMediator/actions/workflows/ci.yml/badge.svg)](https://github.com/berk2k/FlowMediator/actions/workflows/ci.yml)



link: https://www.nuget.org/packages/FlowMediator/

## Overview
FlowMediator is a lightweight mediator library for .NET 8/9, designed to simplify the CQRS and Mediator pattern with minimal setup.
It supports request/response messaging, pipeline behaviors, and domain events out of the box.

Use it when you want:

-Clean separation of concerns in your application

-Simple request/response messaging without boilerplate

-EF Core integration with Domain Events

-A minimal learning-friendly alternative to heavy frameworks

## Features

✅ Request/Response messaging (IRequest<TResponse>, IRequestHandler<TRequest,TResponse>)

✅ Pipeline behaviors (IPipelineBehavior<TRequest,TResponse>) for logging, validation, etc.

✅ Domain Events with EF Core integration (v1.2.0+)

✅ Dependency Injection (DI) extensions for IServiceCollection

✅ Manual or automatic pipeline registration

✅ .NET 8 and .NET 9 support

## Installation
```bash
dotnet add package FlowMediator --version 1.2.0
```

## Example Usage
## 1.Define a Request & Handler
```markdown
// Query
public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

// Handler
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        return user ?? throw new Exception("User not found");
    }
}
```

## 2. Register in DI
```markdown
services.AddFlowMediator(typeof(GetUserByIdQuery).Assembly);
services.AddScoped<IUserRepository, UserRepository>();

// Optional: pipeline behaviors
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
```

## 3. Use Mediator
```markdown
var mediator = provider.GetRequiredService<IMediator>();

var user = await mediator.Send(new GetUserByIdQuery(1));
Console.WriteLine(user.Name); // "User 1"

```

## Domain Events with EF Core
## Base Entity
```markdown
public abstract class BaseEntity
{
    private readonly List<IRequest<Unit>> _domainEvents = new();
    public IReadOnlyCollection<IRequest<Unit>> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IRequest<Unit> domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
```

## DbContext Integration
```markdown
public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
            entity.ClearDomainEvents();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

```

## Example Entity + Event
```markdown
public class User : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public User(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        AddDomainEvent(new UserCreatedEvent(Id, Name));
    }
}

public record UserCreatedEvent(Guid Id, string Name) : IRequest<Unit>;

```

## Event Handler
```markdown
public class UserCreatedEventHandler : IRequestHandler<UserCreatedEvent, Unit>
{
    public Task<Unit> Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[DomainEvent] User created: {notification.Name}");
        return Task.FromResult(Unit.Value);
    }
}


```

## 🤝 Contributing
Contributions, bug reports, and feature requests are welcome!

1. Fork the repo

2. Create a feature branch (git checkout -b feature/my-feature)

3. Commit changes and push

4. Open a Pull Request

## ⚠️ Disclaimer

FlowMediator is a lightweight mediator library designed for hobby projects, learning, and simple applications.  
It does **not** implement any security features by itself.  
Please handle **validation, authorization, and exception management** within your own application.

## 📜 License
Licensed under the MIT License. See [LICENSE](./LICENSE) for details.

