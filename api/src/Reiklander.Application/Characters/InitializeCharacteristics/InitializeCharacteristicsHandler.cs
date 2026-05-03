using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.InitializeCharacteristics;

public class InitializeCharacteristicsHandler(IEventStoreRepository repository)
{
    public async Task Handle(InitializeCharacteristicsCommand command)
    {
        var character = await repository.LoadAsync<Character, CharacterId, Guid>(command.CharacterId);

        character.InitializeCharacteristics();

        await repository.SaveAsync<Character, CharacterId, Guid>(character);
    }
}
