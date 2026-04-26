using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class SpeciesSelectedProjection(EventStoreDbContext context) : IProjectionHandler<SpeciesSelected>
{
    public async Task Handle(SpeciesSelected @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null) return;

        character.Species = @event.Species;
    }
}
