# FlowMediator
link: https://www.nuget.org/packages/FlowMediator/

A lightweight mediator library with pipeline behaviors for .NET 8/9.

Ideal for hobby projects, or learning CQRS patterns without the complexity of bigger frameworks.

Provides request/response messaging and pipeline behaviors like logging and validation with minimal setup.

## Features

✅ Request/Response messaging (IRequest<TResponse>, IRequestHandler<TRequest,TResponse>)

✅ Pipeline behaviors (IPipelineBehavior<TRequest,TResponse>) for cross-cutting concerns (logging, validation, etc.)

✅ Dependency Injection (DI) ready via IServiceCollection extensions

✅ Manual or automatic pipeline registration

✅ .NET 8 and .NET 9 support

## Installation
```bash
dotnet add package FlowMediator --version 1.0.0
```

## Example Usage
```markdown
using FlowMediator.Contracts;
using FlowMediator.Extensions;

// Manual pipeline (you choose order of behaviors)
services.AddFlowMediator(typeof(GetUserByIdQuery).Assembly);
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Or automatic pipeline (handlers + behaviors auto registered)
services.AddFlowMediatorWithBehaviors(typeof(GetUserByIdQuery).Assembly);
```

## Use Mediator
```markdown
var mediator = provider.GetRequiredService<IMediator>();

var user = await mediator.Send(new GetUserByIdQuery(1));
Console.WriteLine(user.Name); // "User 1"
```

## 🤝 Contributing
Contributions are welcome! Please fork the repo and create a PR.

## ⚠️ Disclaimer

FlowMediator is a lightweight mediator library designed for hobby projects, learning, and simple applications.  
It does **not** implement any security features by itself.  
Please handle **validation, authorization, and exception management** within your own application.

## 📜 License
Licensed under the MIT License. See [LICENSE](./LICENSE) for details.

