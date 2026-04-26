using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class ExperienceEarnedProjection(EventStoreDbContext context) : IProjectionHandler<ExperienceEarned>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(ExperienceEarned @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null) return;

        character.Experience += @event.Amount;
    }
}
