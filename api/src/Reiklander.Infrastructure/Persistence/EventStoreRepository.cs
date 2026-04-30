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

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        var events = aggregate.UncommittedEvents;

        if (!events.Any())
            return;

        var currentVersion = await context.Events
            .Where(x => x.AggregateId == Convert.ToString(aggregate.IdValue, CultureInfo.InvariantCulture))
            .Select(x => (int?)x.Version)
            .MaxAsync() ?? 0;

        var version = currentVersion;

        foreach (var e in events)
        {
            version++;

            var entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregate.IdValue.ToString()!,
                AggregateType = aggregate.GetType().Name,
                Version = version,
                EventType = e.GetType().Name,
                Data = JsonSerializer.Serialize(e, e.GetType(), EventJsonOptions),
                OccurredOn = DateTime.UtcNow
            };

            context.Events.Add(entity);

            var domainEvent = Deserialize(entity);
            await dispatcher.Dispatch(domainEvent, Guid.Parse(entity.AggregateId));
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
