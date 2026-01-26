FlowMediator is a lightweight, opinionated mediator library for .NET 8 and .NET 9.

It simplifies **CQRS-style application flow** by explicitly separating
request-based operations (commands and queries)
from event-driven side effects.

FlowMediator provides request/response messaging and pipeline behaviors
with a predictable and transparent execution model.

---

Use FlowMediator when you want:

- Clear separation between application flow and events
- Simple request/response messaging without boilerplate
- Predictable mediator behavior
- A lightweight alternative to generic mediator frameworks


## Installation

```bash
dotnet add package FlowMediator
```

## Core Usage
### Command / Query (SendAsync)

``` csharp
var user = await mediator.SendAsync(new GetUserByIdQuery(1));
```
- Single handler
- Pipeline-enabled (logging, validation, etc.)

### Events (PublishAsync)
``` csharp
await mediator.PublishAsync(new UserCreatedEvent());
```

- Multiple handlers supported
- No response
- Side-effect oriented

Event handlers are executed sequentially and synchronously by default.

## Key Concepts
- SendAsync -> application flow (commands / queries)
- PublishAsync -> event notifications
- Events are not treated as requests
- Pipelines apply only to SendAsync

## Documentation
Full documentation, migration guide, and roadmap are available on GitHub:
👉 https://github.com/berk2k/FlowMediator

License: MIT






