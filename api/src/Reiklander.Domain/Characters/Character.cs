using Reiklander.Domain.Characters.Characteristics;
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

    #region Characteristics
    public CharacteristicState WeaponSkill { get; private set; } = new(0, 0);
    public CharacteristicState BallisticSkill { get; private set; } = new(0, 0);
    public CharacteristicState Strength { get; private set; } = new(0, 0);
    public CharacteristicState Toughness { get; private set; } = new(0, 0);
    public CharacteristicState Initiative { get; private set; } = new(0, 0);
    public CharacteristicState Agility { get; private set; } = new(0, 0);
    public CharacteristicState Dexterity { get; private set; } = new(0, 0);
    public CharacteristicState Intelligence { get; private set; } = new(0, 0);
    public CharacteristicState Willpower { get; private set; } = new(0, 0);
    public CharacteristicState Fellowship { get; private set; } = new(0, 0);
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

    public void AdvanceCharacteristic(CharacteristicType characteristic)
    {
        int cost = GetCharacteristic(characteristic).AdvanceCost;

        if (cost > ExperiencePoints)
            throw new InvalidOperationException("Not enough XP");

        Raise(new ExperienceSpent(cost, $"Characteristic advanced [{characteristic}]"));
        Raise(new CharacteristicAdvanced(characteristic));
    }

    private CharacteristicState GetCharacteristic(CharacteristicType characteristic)
    {
        return characteristic switch
        {
            CharacteristicType.WeaponSkill => WeaponSkill,
            CharacteristicType.BallisticSkill => BallisticSkill,
            CharacteristicType.Strength => Strength,
            CharacteristicType.Toughness => Toughness,
            CharacteristicType.Initiative => Initiative,
            CharacteristicType.Agility => Agility,
            CharacteristicType.Dexterity => Dexterity,
            CharacteristicType.Intelligence => Intelligence,
            CharacteristicType.Willpower => Willpower,
            CharacteristicType.Fellowship => Fellowship,
            _ => throw new InvalidOperationException("Unknown CharacteristicType"),
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

            case CharacteristicAdvanced advance:
                GetCharacteristic(advance.Characteristic).Advance();

                break;
        }
    }
}
