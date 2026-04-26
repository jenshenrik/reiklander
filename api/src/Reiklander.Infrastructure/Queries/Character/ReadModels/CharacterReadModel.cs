using Reiklander.Infrastructure.Queries.Characters.ReadModels;

namespace Reiklander.Infrastructure.Queries.Character.ReadModels;

public class CharacterReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Experience { get; set; }

    public AttributeReadModel WeaponSkill { get; set; } = new();
    public AttributeReadModel BallisticSkill { get; set; } = new();
    public AttributeReadModel Strength { get; set; } = new();
    public AttributeReadModel Toughness { get; set; } = new();
    public AttributeReadModel Initiative { get; set; } = new();
    public AttributeReadModel Agility { get; set; } = new();
    public AttributeReadModel Dexterity { get; set; } = new();
    public AttributeReadModel Intelligence { get; set; } = new();
    public AttributeReadModel Willpower { get; set; } = new();
    public AttributeReadModel Fellowship { get; set; } = new();
}
