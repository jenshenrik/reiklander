using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.SelectSpecies;

public class SelectSpeciesHandler(IEventStoreRepository eventStoreRepository, ISpeciesRepository speciesRepository)
{
    private readonly IEventStoreRepository eventStoreRepository = eventStoreRepository;
    private readonly ISpeciesRepository speciesRepository = speciesRepository;

    public async Task Handle(Guid characterId, string speciesName)
    {
        var character = await eventStoreRepository.LoadAsync<Character>(characterId);
        var species = await speciesRepository.GetAsync(speciesName);

        character.SelectSpecies(species.Name);

        await eventStoreRepository.SaveAsync(character);
    }
}
