using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure;

public class EventStoreRepository(EventStoreDbContext context, ProjectionDispatcher dispatcher) : IEventStoreRepository
{
    public async Task SaveAsync(AggregateRoot aggregate)
    {
        var events = aggregate.UncommittedEvents;

        if (!events.Any())
            return;

        var currentVersion = await context.Events
            .Where(x => x.AggregateId == aggregate.Id)
            .Select(x => (int?)x.Version)
            .MaxAsync() ?? 0;

        var version = currentVersion;

        foreach (var e in events)
        {
            Console.WriteLine($"aggregate id {aggregate.Id}");
            version++;

            var entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregate.Id,
                AggregateType = aggregate.GetType().Name,
                Version = version,
                EventType = e.GetType().Name,
                Data = JsonSerializer.Serialize(e, e.GetType()),
                OccurredOn = DateTime.UtcNow
            };

            context.Events.Add(entity);

            var domainEvent = Deserialize(entity);
            await dispatcher.Dispatch(domainEvent, entity.AggregateId);
        }

        await context.SaveChangesAsync();

        aggregate.MarkEventsCommitted();
    }

    public async Task<T> LoadAsync<T>(Guid id) where T : AggregateRoot, new()
    {
        var events = await context.Events
            .Where(x => x.AggregateId == id)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var domainEvents = events.Select(Deserialize);

        var aggregate = new T();
        aggregate.LoadFromHistory(domainEvents);

        return aggregate;
    }

    private IDomainEvent Deserialize(EventEntity e)
    {
        return e.EventType switch
        {
            nameof(ExperienceEarned) =>
                JsonSerializer.Deserialize<ExperienceEarned>(e.Data)!,

            nameof(CharacterCreated) =>
                JsonSerializer.Deserialize<CharacterCreated>(e.Data)!,

            nameof(NameCharacter) =>
                JsonSerializer.Deserialize<NameCharacter>(e.Data)!,

            nameof(AttributeAdvanced) =>
                JsonSerializer.Deserialize<AttributeAdvanced>(e.Data)!,

            _ => throw new Exception("Unknown event type")
        };
    }
}
