using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Characteristics;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;
using Reiklander.Infrastructure.Queries.Characters.ReadModels;

namespace Reiklander.Infrastructure.Projections.Characters;

public class CharacteristicAdvancedProjection(EventStoreDbContext context) : IProjectionHandler<CharacteristicAdvanced>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(CharacteristicAdvanced @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null)
            return;

        var characteristic = GetCharacteristic(character, @event.Characteristic);

        var state = CharacteristicState.From(characteristic.Value, characteristic.Advances);
        var updatedCharacteristicState = state.Advance();

        characteristic.Value = updatedCharacteristicState.Value;
        characteristic.Bonus = updatedCharacteristicState.Bonus;
        characteristic.CostToAdvance = updatedCharacteristicState.GetAdvanceCost();
        characteristic.Advances = updatedCharacteristicState.Advances;
    }

    private static CharacteristicReadModel GetCharacteristic(CharacterReadModel character, CharacteristicType characteristic) =>
         characteristic switch
         {
             CharacteristicType.WeaponSkill => character.WeaponSkill,
             CharacteristicType.BallisticSkill => character.BallisticSkill,
             CharacteristicType.Strength => character.Strength,
             CharacteristicType.Toughness => character.Toughness,
             CharacteristicType.Initiative => character.Initiative,
             CharacteristicType.Agility => character.Agility,
             CharacteristicType.Dexterity => character.Dexterity,
             CharacteristicType.Intelligence => character.Intelligence,
             CharacteristicType.Willpower => character.Willpower,
             CharacteristicType.Fellowship => character.Fellowship,
             _ => throw new ArgumentOutOfRangeException(),
         };
}
