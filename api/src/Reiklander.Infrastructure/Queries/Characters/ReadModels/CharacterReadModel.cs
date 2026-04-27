namespace Reiklander.Infrastructure.Queries.Characters.ReadModels;

public class CharacterReadModel
{
    public Guid Id { get; set; }
    public string Species { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Experience { get; set; }
    public int ExperienceSpent { get; set; }
    public int ExperienceTotal { get; set; }

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
