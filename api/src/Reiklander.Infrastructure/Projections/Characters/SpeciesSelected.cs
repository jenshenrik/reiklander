using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class SpeciesSelectedProjection(EventStoreDbContext context) : IProjectionHandler<SpeciesSelected, Guid>
{
    public async Task Handle(SpeciesSelected @event, IEventEnvelope<Guid> envelope)
    {
        var character = await context.Characters.FindAsync(envelope.AggregateId.Value);

        if (character == null) return;

        character.Species = @event.Species;
    }
}
