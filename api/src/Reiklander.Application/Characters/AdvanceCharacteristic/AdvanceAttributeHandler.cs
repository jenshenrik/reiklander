using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.AdvanceCharacteristic;

public class AdvanceCharacteristicHandler(IEventStoreRepository repository)
{
    private readonly IEventStoreRepository repository = repository;

    public async Task Handle(AdvanceCharacteristicCommand command)
    {
        var character = await repository.LoadAsync<Character>(command.CharacterId);

        character.AdvanceCharacteristic(command.Characteristic);

        await repository.SaveAsync(character);
    }
}

