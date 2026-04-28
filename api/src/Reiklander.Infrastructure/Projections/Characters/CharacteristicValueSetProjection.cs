using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Characteristics;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;

namespace Reiklander.Infrastructure.Projections.Characters;

public class CharacteristicValueSetProjection(EventStoreDbContext context) : IProjectionHandler<CharacteristicValueSet>
{
    public async Task Handle(CharacteristicValueSet @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null)
            return;

        CharacteristicState state;
        switch (@event.Characteristic)
        {
            case CharacteristicType.WeaponSkill:
                state = CharacteristicState.From(@event.Value, character.WeaponSkill.Advances);
                character.WeaponSkill.Value = state.Value;
                character.WeaponSkill.Bonus = state.Bonus;
                break;
            case CharacteristicType.BallisticSkill:
                state = CharacteristicState.From(@event.Value, character.BallisticSkill.Advances);
                character.BallisticSkill.Value = state.Value;
                character.BallisticSkill.Bonus = state.Bonus;
                break;
            case CharacteristicType.Strength:
                state = CharacteristicState.From(@event.Value, character.Strength.Advances);
                character.Strength.Value = state.Value;
                character.Strength.Bonus = state.Bonus;
                break;
            case CharacteristicType.Toughness:
                state = CharacteristicState.From(@event.Value, character.Toughness.Advances);
                character.Toughness.Value = state.Value;
                character.Toughness.Bonus = state.Bonus;
                break;
            case CharacteristicType.Initiative:
                state = CharacteristicState.From(@event.Value, character.Initiative.Advances);
                character.Initiative.Value = state.Value;
                character.Initiative.Bonus = state.Bonus;
                break;
            case CharacteristicType.Agility:
                state = CharacteristicState.From(@event.Value, character.Agility.Advances);
                character.Agility.Value = state.Value;
                character.Agility.Bonus = state.Bonus;
                break;
            case CharacteristicType.Dexterity:
                state = CharacteristicState.From(@event.Value, character.Dexterity.Advances);
                character.Dexterity.Value = state.Value;
                character.Dexterity.Bonus = state.Bonus;
                break;
            case CharacteristicType.Intelligence:
                state = CharacteristicState.From(@event.Value, character.Intelligence.Advances);
                character.Intelligence.Value = state.Value;
                character.Intelligence.Bonus = state.Bonus;
                break;
            case CharacteristicType.Willpower:
                state = CharacteristicState.From(@event.Value, character.Willpower.Advances);
                character.Willpower.Value = state.Value;
                character.Willpower.Bonus = state.Bonus;

                break;
            case CharacteristicType.Fellowship:
                state = CharacteristicState.From(@event.Value, character.Fellowship.Advances);
                character.Fellowship.Value = state.Value;
                character.Fellowship.Bonus = state.Bonus;
                break;
        }
        ;
    }
}
