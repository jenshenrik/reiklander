using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class NameCharacterProjection(EventStoreDbContext context) : IProjectionHandler<NameCharacter, Guid>
{
    public async Task Handle(NameCharacter @event, IEventEnvelope<Guid> envelope)
    {
        var character = await context.Characters.FindAsync(envelope.AggregateId.Value);

        if (character == null) return;

        character.Name = @event.Name;
    }
}
