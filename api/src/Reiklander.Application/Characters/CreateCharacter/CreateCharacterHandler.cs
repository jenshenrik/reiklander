using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.CreateCharacter;

public class CreateCharacterHandler(IEventStoreRepository repository)
{
    public async Task<Guid> Handle(CreateCharacterCommand command)
    {
        var character = Character.Create();

        await repository.SaveAsync(character);

        return character.Id.Value;
    }
}
