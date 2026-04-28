namespace Reiklander.Infrastructure.Queries.Characters.ReadModels;

public class CharacterReadModel
{
    public Guid Id { get; set; }
    public string Species { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Experience { get; set; }
    public int ExperienceSpent { get; set; }
    public int ExperienceTotal { get; set; }

    public CharacteristicReadModel WeaponSkill { get; set; } = new();
    public CharacteristicReadModel BallisticSkill { get; set; } = new();
    public CharacteristicReadModel Strength { get; set; } = new();
    public CharacteristicReadModel Toughness { get; set; } = new();
    public CharacteristicReadModel Initiative { get; set; } = new();
    public CharacteristicReadModel Agility { get; set; } = new();
    public CharacteristicReadModel Dexterity { get; set; } = new();
    public CharacteristicReadModel Intelligence { get; set; } = new();
    public CharacteristicReadModel Willpower { get; set; } = new();
    public CharacteristicReadModel Fellowship { get; set; } = new();
}
