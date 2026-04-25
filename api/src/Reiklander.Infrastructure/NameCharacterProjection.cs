using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;

namespace Reiklander.Infrastructure;

public class NameCharacterProjection(EventStoreDbContext context) : IProjectionHandler<NameCharacter>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(NameCharacter @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        Console.WriteLine($"old name {character.Name}");
        if (character == null) return;

        character.Name = @event.Name;
        Console.WriteLine($"new name {character.Name}");
    }
}
