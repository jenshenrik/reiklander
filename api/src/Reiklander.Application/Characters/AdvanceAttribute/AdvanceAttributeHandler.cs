using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.AdvanceAttribute;

public class AdvanceAttributeHandler(IEventStoreRepository repository)
{
    private readonly IEventStoreRepository repository = repository;

    public async Task Handle(AdvanceAttributeCommand command)
    {
        var character = await repository.LoadAsync<Character>(command.CharacterId);

        character.AdvanceAttribute(command.Attribute);

        await repository.SaveAsync(character);
    }
}
