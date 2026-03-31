# FlowMediator.Console

A working example demonstrating core FlowMediator features.

## What this example covers

- Query handling with `SendAsync`
- Pipeline behaviors (logging + validation) in explicit order
- Validation failure scenario
- Event publishing with `PublishAsync`

## How to run
```bash
dotnet run
```

## Project structure
```
FlowMediator.Console/
    Behaviors/
        LoggingBehavior.cs     → logs every request
        ValidationBehavior.cs  → validates via FluentValidation
    Entities/
        User.cs
    EventHandlers/
        UserCreatedEventHandler.cs
    Events/
        UserCreatedEvent.cs
    Queries/
        GetUserByIdQuery.cs
        GetUserByIdHandler.cs
    Repositories/
        IUserRepository.cs
    Validators/
        GetUserByIdQueryValidator.cs
    Program.cs                 → entry point, DI registration
```

## Expected output
```
---- QUERY TEST ----
User Id: 1, Name: John Doe
Validation failed:
 - Id: Id must be greater than 0

---- EVENT TEST ----
User created: John Doe

Demo finished successfully.
```