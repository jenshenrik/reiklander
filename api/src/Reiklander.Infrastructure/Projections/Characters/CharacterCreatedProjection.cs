using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;
using Reiklander.Infrastructure.Queries.Characters.ReadModels;

namespace Reiklander.Infrastructure.Projections.Characters;

public class CharacterCreatedProjection(EventStoreDbContext context) : IProjectionHandler<CharacterCreated>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(CharacterCreated @event, Guid aggregateId)
    {
        context.Characters.Add(new CharacterReadModel
        {
            Id = aggregateId,
            Experience = 0
        });
    }
}
