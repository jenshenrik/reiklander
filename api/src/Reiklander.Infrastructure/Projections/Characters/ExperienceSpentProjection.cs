using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class ExperienceSpentProjection(EventStoreDbContext context) : IProjectionHandler<ExperienceSpent>
{
    public async Task Handle(ExperienceSpent @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null)
            return;

        character.Experience -= @event.Amount;
        character.ExperienceSpent += @event.Amount;
    }
}
