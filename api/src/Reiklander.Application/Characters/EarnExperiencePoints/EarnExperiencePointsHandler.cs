using Reiklander.Domain.Characters;
using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Characters.EarnExperiencePoints;

public class EarnExperiencePointsHandler(IEventStoreRepository repository)
{
    private readonly IEventStoreRepository repository = repository;

    public async Task Handle(EarnExperiencePointsCommand command)
    {
        var character = await repository.LoadAsync<Character, CharacterId, Guid>(command.CharacterId);

        character.EarnExperience(command.Amount);

        await repository.SaveAsync(character);
    }
}
