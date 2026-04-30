# TODO

Deferred tasks tracked for later. Each item links to the relevant code.

- [ ] Fix `Location` header path in `POST /characters` — currently `/characters/{id}`, should be `/api/v{version}/characters/{id}` (returns 404 if followed). See `api/src/Reiklander.Api/Endpoints/Characters/CharactersEndpointModule.cs:100`. Prefer `LinkGenerator` with a named route over hardcoding the version.
- [ ] Remove leftover `Console.WriteLine($"{id}");` debug output in `NameCharacter` endpoint (`api/src/Reiklander.Api/Endpoints/Characters/CharactersEndpointModule.cs:115`), or replace with structured `ILogger` logging if intentional.
- [ ] Add `Guid Id` field to `CharacterResponse` (`api/src/Reiklander.Contracts/Characters/CharacterResponse.cs`) and populate it in `CharacterQueries` so clients can recover the ID from the payload.
- [ ] Generalize `ProjectionDispatcher` to stop hard-coding `Guid.Parse(entity.AggregateId)` (`api/src/Reiklander.Infrastructure/Projections/ProjectionDispatcher.cs`). Currently safe (only `Character` aggregate exists, Guid-backed); becomes a blocker when a non-Guid-backed aggregate is introduced. Consider `IProjectionHandler<TEvent, TId>` or reflective `IAggregateId<,>` resolution. Until then, add a code comment documenting the assumption.
- [ ] Document the Domain-only typed-ID boundary — typed IDs live inside `Reiklander.Domain` only; command records carry typed IDs; endpoints/queries/read-models speak `Guid`; conversion happens at the endpoint seam via `CharacterId.From(id)`. Either a short ADR (e.g. `docs/adr/0001-strongly-typed-ids-boundary.md`) or a header comment in `api/src/Reiklander.Domain/Kernel/IAggregateId.cs`.

## DDD violations

Existing leaks across architectural boundaries. Independent of any single feature; flagged here so they're not lost.

- [ ] Retire `AggregateRoot.IdValue` (`api/src/Reiklander.Domain/Kernel/AggregateRoot.cs:9,14`). It exposes the ID's primitive as `object` purely so Infrastructure (`EventStoreRepository`) can persist it without knowing the typed-ID type — a persistence concern leaking into the domain. Preferred fix: make `IEventStoreRepository.SaveAsync` generic over `<TAggregate, TId, TPrimitive>` (mirroring `LoadAsync`) so the typed `Id` is accessible directly, then drop `IdValue`.
- [ ] Move projection contracts out of the Application layer. `IProjectionHandler<TEvent>` (`api/src/Reiklander.Application/Kernel/IProjectionHandler.cs`) is a CQRS read-side concern; its only implementations live in `Reiklander.Infrastructure/Projections/`, and they update Infrastructure-owned read models. The interface belongs in Infrastructure (or a dedicated read-side project), not Application.
- [ ] Decouple write path from projection updates. `EventStoreRepository.SaveAsync` (`api/src/Reiklander.Infrastructure/Persistence/EventStoreRepository.cs:50`) calls `dispatcher.Dispatch(...)` synchronously inside the same transaction as the event append. Strict CQRS publishes events to a bus and lets projections subscribe asynchronously. Current synchronous coupling is pragmatic, not principled — flag for a future async-dispatch refactor.
- [ ] Formalize the event dispatch contract with an envelope. `ProjectionDispatcher.Dispatch(IDomainEvent, Guid)` passes an ad-hoc `(event, id)` tuple, hard-codes `Guid.Parse(entity.AggregateId)`, and discards the metadata `EventEntity` already stores (version, occurred-on, aggregate type). Replace with an `IEventEnvelope<TPrimitive>` carrying the typed aggregate ID, version, occurred-on, etc. Subsumes the existing "generalize `ProjectionDispatcher`" item above.
