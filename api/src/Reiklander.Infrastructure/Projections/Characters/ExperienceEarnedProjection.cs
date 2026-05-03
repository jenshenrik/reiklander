using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class ExperienceEarnedProjection(EventStoreDbContext context) : IProjectionHandler<ExperienceEarned, Guid>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(ExperienceEarned @event, IEventEnvelope<Guid> envelope)
    {
        var character = await context.Characters.FindAsync(envelope.AggregateId.Value);

        if (character == null) return;

        character.Experience += @event.Amount;
        character.ExperienceTotal += @event.Amount;
    }
}
