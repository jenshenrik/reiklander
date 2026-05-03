using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;
using Reiklander.Infrastructure.Persistence.Serialization;
using Reiklander.Infrastructure.Projections;

namespace Reiklander.Infrastructure.Persistence;

public class EventStoreRepository(EventStoreDbContext context, ProjectionDispatcher dispatcher) : IEventStoreRepository
{
    private static readonly JsonSerializerOptions EventJsonOptions = new()
    {
        Converters = { new AggregateIdJsonConverterFactory() }
    };

    public async Task SaveAsync<TAggregate, TId, TPrimitive>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId, TPrimitive>
        where TId : struct, IAggregateId<TId, TPrimitive>
        where TPrimitive : notnull
    {
        var events = aggregate.UncommittedEvents;

        if (!events.Any())
            return;

        var aggregateIdString = Convert.ToString(aggregate.Id.Value, CultureInfo.InvariantCulture)!;

        var currentVersion = await context.Events
            .Where(x => x.AggregateId == aggregateIdString)
            .Select(x => (int?)x.Version)
            .MaxAsync() ?? 0;

        var version = currentVersion;

        foreach (var e in events)
        {
            version++;

            var entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregateIdString,
                AggregateType = aggregate.GetType().Name,
                Version = version,
                EventType = e.GetType().Name,
                Data = JsonSerializer.Serialize(e, e.GetType(), EventJsonOptions),
                OccurredOn = DateTime.UtcNow
            };

            context.Events.Add(entity);

            var domainEvent = Deserialize(entity);

            var envelope = new EventEnvelope<TPrimitive>(
                    domainEvent,
                    aggregate.Id,
                    entity.Version,
                    entity.OccurredOn
                    );

            await dispatcher.Dispatch(envelope);
        }

        await context.SaveChangesAsync();

        aggregate.MarkEventsCommitted();
    }

    public async Task<TAggregate> LoadAsync<TAggregate, TId, TPrimitive>(TId id)
        where TAggregate : AggregateRoot<TId, TPrimitive>, new()
        where TId : struct, IAggregateId<TId, TPrimitive>
        where TPrimitive : notnull
    {
        var events = await context.Events
            .Where(x => x.AggregateId == Convert.ToString(id.Value, CultureInfo.InvariantCulture))
            .OrderBy(x => x.Version)
            .ToListAsync();

        var domainEvents = events.Select(Deserialize);

        var aggregate = new TAggregate();
        aggregate.LoadFromHistory(domainEvents);

        return aggregate;
    }

    private IDomainEvent Deserialize(EventEntity e)
    {
        return e.EventType switch
        {
            nameof(CharacterCreated) =>
                JsonSerializer.Deserialize<CharacterCreated>(e.Data, EventJsonOptions)!,

            nameof(SpeciesSelected) =>
                JsonSerializer.Deserialize<SpeciesSelected>(e.Data, EventJsonOptions)!,

            nameof(NameCharacter) =>
                JsonSerializer.Deserialize<NameCharacter>(e.Data, EventJsonOptions)!,

            nameof(ExperienceEarned) =>
                JsonSerializer.Deserialize<ExperienceEarned>(e.Data, EventJsonOptions)!,

            nameof(ExperienceSpent) =>
                JsonSerializer.Deserialize<ExperienceSpent>(e.Data, EventJsonOptions)!,

            nameof(CharacteristicValueSet) =>
                JsonSerializer.Deserialize<CharacteristicValueSet>(e.Data, EventJsonOptions)!,

            nameof(CharacteristicAdvanced) =>
                JsonSerializer.Deserialize<CharacteristicAdvanced>(e.Data, EventJsonOptions)!,

            _ => throw new Exception("Unknown event type")
        };
    }
}
