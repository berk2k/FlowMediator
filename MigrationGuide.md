# Migrating from FlowMediator v1.x to v2.0

FlowMediator v2 introduces a more explicit and opinionated execution model.
This guide explains how to migrate existing applications.

---

## 1) Domain Events

### v1

```csharp
public interface IDomainEvent : IRequest<Unit> { }

await mediator.SendAsync(new UserCreatedEvent(...));
```

### v2
```csharp
public interface IDomainEvent : IEvent { }

await mediator.PublishAsync(new UserCreatedEvent(...));
```

## 2) Event Handlers

### v1
```csharp
public class UserCreatedHandler
    : IRequestHandler<UserCreatedEvent, Unit>
{
    public Task<Unit> Handle(...) { }
}
```
### v2
```csharp
public class UserCreatedHandler
    : IEventHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent @event, CancellationToken ct)
    {
        // side effects
    }
}
```
## 3) Send vs Publish
| Purpose                     | Method         |
| --------------------------- | -------------- |
| Command / Query             | `SendAsync`    |
| Domain / Integration Events | `PublishAsync` |

Events can no longer be sent using SendAsync.

## 4) Pipelines
- Pipelines apply only to SendAsync
- Events are executed outside the pipeline
- This enables retries, compensation, and observability in future versions

## 5) Dependency Injection

No changes required.
Event handlers are discovered automatically.
