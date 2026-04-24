using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure;

public class EventStoreRepository
{
    private readonly EventStoreDbContext _context;

    public EventStoreRepository(EventStoreDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        var events = aggregate.UncommittedEvents;

        if (!events.Any())
            return;

        var currentVersion = await _context.Events
            .Where(x => x.AggregateId == aggregate.Id)
            .Select(x => (int?)x.Version)
            .MaxAsync() ?? 0;

        var version = currentVersion;

        foreach (var e in events)
        {
            version++;

            _context.Events.Add(new EventEntity
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

        await _context.SaveChangesAsync();

        aggregate.MarkEventsCommitted();
    }

    public async Task<Character> LoadAsync(Guid id)
    {
        var events = await _context.Events
            .Where(x => x.AggregateId == id)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var domainEvents = events.Select(Deserialize);

        var character = new Character();
        character.LoadFromHistory(domainEvents);

        return character;
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
