using Reiklander.Contracts.Attributes;

namespace Reiklander.Api.Endpoints.Characters.Contracts;

public record CharacterResponse(
    string Name,
    int Xp,
    AttributeResponse WeaponSkill,
    AttributeResponse BallisticSkill,
    AttributeResponse Strength,
    AttributeResponse Toughness,
    AttributeResponse Initiative,
    AttributeResponse Agility,
    AttributeResponse Dexterity,
    AttributeResponse Intelligence,
    AttributeResponse Willpower,
    AttributeResponse Fellowship
);
