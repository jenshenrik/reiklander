using Reiklander.Domain.Characters.Attributes;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public class Character : AggregateRoot
{
    public Character() { }

    public static Character Create(Guid id)
    {
        if (id == Guid.Empty)
            throw new InvalidDataException("Character ID is not a valid GUID");

        var character = new Character
        {
            Id = id,
        };

        character.Raise(new CharacterCreated(id));

        return character;
    }

    public string Name { get; private set; } = default!;

    public int ExperiencePoints { get; private set; }

    public AttributeState WeaponSkill { get; private set; } = new(0, 0);
    public AttributeState BallisticSkill { get; private set; } = new(0, 0);
    public AttributeState Strength { get; private set; } = new(0, 0);
    public AttributeState Toughness { get; private set; } = new(0, 0);
    public AttributeState Initiative { get; private set; } = new(0, 0);
    public AttributeState Agility { get; private set; } = new(0, 0);
    public AttributeState Dexterity { get; private set; } = new(0, 0);
    public AttributeState Intelligence { get; private set; } = new(0, 0);
    public AttributeState Willpower { get; private set; } = new(0, 0);
    public AttributeState Fellowship { get; private set; } = new(0, 0);

    public void NameCharacter(string name)
    {
        Raise(new NameCharacter(name));
    }

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
        return attribute switch
        {
            AttributeType.WeaponSkill => WeaponSkill,
            AttributeType.BallisticSkill => BallisticSkill,
            AttributeType.Strength => Strength,
            AttributeType.Toughness => Toughness,
            AttributeType.Initiative => Initiative,
            AttributeType.Agility => Agility,
            AttributeType.Dexterity => Dexterity,
            AttributeType.Intelligence => Intelligence,
            AttributeType.Willpower => Willpower,
            AttributeType.Fellowship => Fellowship,
            _ => throw new InvalidOperationException("Unknown attribute type"),
        };
    }

    protected override void Apply(IDomainEvent e)
    {
        switch (e)
        {
            case NameCharacter name:
                Name = name.Name;

                break;

            case ExperienceEarned xp:
                ExperiencePoints += xp.Amount;

                break;

            case CharacterCreated character:
                Id = character.Id;

                break;

            case AttributeAdvanced advance:
                ExperiencePoints -= advance.ExperienceCost;

                GetAttribute(advance.Attribute).Advance();

                break;
        }
    }
}
