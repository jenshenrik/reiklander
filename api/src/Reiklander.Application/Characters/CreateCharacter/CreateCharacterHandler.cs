using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.CreateCharacter;

public class CreateCharacterHandler(IEventStoreRepository repository)
{
    public async Task<Guid> Handle(CreateCharacterCommand command)
    {
        var id = Guid.NewGuid();

        var character = Character.Create(id, command.Name);

        await repository.SaveAsync(character);

        return id;
    }
}
