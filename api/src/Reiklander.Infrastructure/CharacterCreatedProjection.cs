using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;

namespace Reiklander.Infrastructure;

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
