using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure;

public class EventStoreRepository(EventStoreDbContext context) : IEventStoreRepository
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
            version++;

            context.Events.Add(new EventEntity
            {
                Id = Guid.NewGuid(),
                AggregateId = aggregate.Id,
                AggregateType = aggregate.GetType().Name,
                Version = version,
                EventType = e.GetType().Name,
                Data = JsonSerializer.Serialize(e),
                OccurredOn = DateTime.UtcNow
            });
        }

        await context.SaveChangesAsync();

        aggregate.MarkEventsCommitted();
    }

    // public async Task<Character> LoadAsync(Guid id)
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

            _ => throw new Exception("Unknown event type")
        };
    }
}
