using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.SelectSpecies;

public class SelectSpeciesHandler(IEventStoreRepository eventStoreRepository, ISpeciesRepository speciesRepository)
{
    private readonly IEventStoreRepository eventStoreRepository = eventStoreRepository;
    private readonly ISpeciesRepository speciesRepository = speciesRepository;

    public async Task Handle(SelectSpeciesCommand command)
    {
        var character = await eventStoreRepository.LoadAsync<Character, CharacterId, Guid>(command.CharacterId);
        var species = await speciesRepository.GetAsync(command.SpeciesIdentifier);

        character.SelectSpecies(species.Name);

        await eventStoreRepository.SaveAsync(character);
    }
}
