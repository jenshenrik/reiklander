using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public class Character : AggregateRoot
{
    public Character() { }

    public static Character Create(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new InvalidDataException("Character ID is not a valid GUID");

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidDataException("Character name cannot be empty");


        var character = new Character
        {
            Id = id,
            Name = name
        };

        character.Raise(new CharacterCreated(id, name));

        return character;
    }

    public string Name { get; private set; } = default!;

    public int ExperiencePoints { get; private set; }

    public AttributeState WeaponSkill { get; private set; } = new();
    public AttributeState BallisticSkill { get; private set; } = new();
    public AttributeState Strength { get; private set; } = new();
    public AttributeState Toughness { get; private set; } = new();
    public AttributeState Initiative { get; private set; } = new();
    public AttributeState Agility { get; private set; } = new();
    public AttributeState Dexterity { get; private set; } = new();
    public AttributeState Intelligence { get; private set; } = new();
    public AttributeState Willpower { get; private set; } = new();
    public AttributeState Fellowship { get; private set; } = new();

    public void EarnExperience(int amount)
    {
        Raise(new ExperienceEarned(amount));
    }

    public void AdvanceAttribute(AttributeType attribute)
    {
        int cost = GetAttribute(attribute).GetAdvanceCost();

        if (cost > ExperiencePoints)
            throw new InvalidOperationException("Not enough XP");

        Raise(new AttributeAdvanced(attribute, cost));
    }

    private AttributeState GetAttribute(AttributeType attribute)
    {
        switch (attribute)
        {
            case AttributeType.WeaponSkill:
                return WeaponSkill;
            case AttributeType.BallisticSkill:
                return BallisticSkill;
            case AttributeType.Toughness:
                return Toughness;
            case AttributeType.Initiative:
                return Initiative;
            case AttributeType.Agility:
                return Agility;
            case AttributeType.Dexterity:
                return Dexterity;
            case AttributeType.Intelligence:
                return Intelligence;
            case AttributeType.Willpower:
                return Willpower;
            case AttributeType.Fellowship:
                return Fellowship;
        }

        throw new InvalidOperationException("Unknown attribute type");
    }

    protected override void Apply(IDomainEvent e)
    {
        switch (e)
        {
            case ExperienceEarned xp:
                ExperiencePoints += xp.Amount;

                break;

            case CharacterCreated character:
                Id = character.Id;
                Name = character.Name;

                break;

            case AttributeAdvanced advance:
                ExperiencePoints -= advance.ExperienceCost;

                GetAttribute(advance.Attribute).Advance();

                break;
        }
    }
}
