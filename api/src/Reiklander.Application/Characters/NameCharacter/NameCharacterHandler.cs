using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.NameCharacter;

public class NameCharacterHandler(IEventStoreRepository repository)
{
    private readonly IEventStoreRepository repository = repository;

    public async Task Handle(NameCharacterCommand command)
    {
        var character = await repository.LoadAsync<Character, CharacterId, Guid>(command.CharacterId);

        character.NameCharacter(command.Name);

        await repository.SaveAsync<Character, CharacterId, Guid>(character);
    }
}

