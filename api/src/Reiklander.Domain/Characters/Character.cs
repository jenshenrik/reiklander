using Reiklander.Domain.Characters.Attributes;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public class Character : AggregateRoot
{
    public Character() { }


    public string Species { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public int ExperiencePoints { get; private set; }
    public int ExperiencePointsSpent { get; private set; }
    public int ExperiencePointsTotal => ExperiencePoints + ExperiencePointsSpent;

    #region Attributes
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
    #endregion

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

    public void SelectSpecies(string speciesName)
    {
        if (!string.IsNullOrWhiteSpace(Species))
            throw new InvalidOperationException("Species already selected");

        Raise(new SpeciesSelected(speciesName));
    }

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

        Raise(new ExperienceSpent(cost, $"Attribute advanced [{attribute}]"));
        Raise(new AttributeAdvanced(attribute));
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
            case SpeciesSelected species:
                Species = species.Species;

                break;

            case NameCharacter name:
                Name = name.Name;

                break;

            case ExperienceEarned xp:
                ExperiencePoints += xp.Amount;

                break;

            case ExperienceSpent xp:
                ExperiencePoints -= xp.Amount;
                ExperiencePointsSpent += xp.Amount;

                break;

            case CharacterCreated character:
                Id = character.Id;

                break;

            case AttributeAdvanced advance:
                GetAttribute(advance.Attribute).Advance();

                break;
        }
    }
}
