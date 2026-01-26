# FlowMediator v2.0.0

FlowMediator v2.0 introduces a **clear and explicit separation between application flow and event flow**.

This release is a **conceptual milestone**.
It does not add more features — it fixes the model.

> Events are facts, not commands.

---

## Highlights

- Explicit **Send vs Publish** model
- Events are no longer treated as requests
- Multiple event handlers supported
- Cleaner and more predictable execution flow
- Stronger foundation for observability, retries, and outbox patterns
- Event handlers run in-process and sequentially by default (order not guaranteed unless controlled; stop-on-first-error).

---

## Breaking Changes

- Domain events no longer implement `IRequest<Unit>`
- Events can no longer be sent via `SendAsync`
- Event handlers no longer return `Unit`
- Event execution is no longer part of the pipeline
- Compile-time separation between requests and events

---

## What Stayed the Same

- `IRequest / IRequestHandler` model
- Pipeline behaviors for `SendAsync`
- Assembly scanning and DI registration
- Manual or automatic pipeline configuration
- .NET 8 and .NET 9 support

---

## Send vs Publish

| Purpose | Method | Handlers | Pipeline |
|------|------|------|------|
| Command / Query | `SendAsync` | Single | Yes |
| Event | `PublishAsync` | Multiple | No |

---

### Event Dispatch Semantics (v2)

- `PublishAsync` runs handlers **in-process** and **sequentially** by default.
- **Handler order is not guaranteed** unless explicitly controlled via registration/ordering.
- If any handler throws, dispatch **stops** and the exception is **re-thrown** (remaining handlers won’t run).

## What’s Next

- FlowContext (CorrelationId, UserId, Metadata)
- Step-based execution model
- Built-in observability and retry support

FlowMediator does not try to be everything.
It supports the **right things**, explicitly.
