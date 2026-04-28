using Reiklander.Contracts.Characteristics;

namespace Reiklander.Contracts.Characters;

public record CharacterResponse(
    string Species,
    string Name,
    int ExperiencePoints,
    int ExperiencePointsSpent,
    int ExperiencePointsTotal,
    CharacteristicResponse WeaponSkill,
    CharacteristicResponse BallisticSkill,
    CharacteristicResponse Strength,
    CharacteristicResponse Toughness,
    CharacteristicResponse Initiative,
    CharacteristicResponse Agility,
    CharacteristicResponse Dexterity,
    CharacteristicResponse Intelligence,
    CharacteristicResponse Willpower,
    CharacteristicResponse Fellowship
);
