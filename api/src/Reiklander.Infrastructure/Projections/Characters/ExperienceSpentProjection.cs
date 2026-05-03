using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class ExperienceSpentProjection(EventStoreDbContext context) : IProjectionHandler<ExperienceSpent, Guid>
{
    public async Task Handle(ExperienceSpent @event, IEventEnvelope<Guid> envelope)
    {
        var character = await context.Characters.FindAsync(envelope.AggregateId.Value);

        if (character == null)
            return;

        character.Experience -= @event.Amount;
        character.ExperienceSpent += @event.Amount;
    }
}
