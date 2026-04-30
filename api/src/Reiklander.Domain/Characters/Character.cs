using Reiklander.Domain.Characters.Characteristics;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public class Character : AggregateRoot<CharacterId, Guid>
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

    public static Character Create()
    {
        var character = new Character();

        character.Raise(new CharacterCreated(CharacterId.Create()));

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

    public void InitializeCharacteristics()
    {
        if (string.IsNullOrWhiteSpace(Species))
            throw new InvalidOperationException("Characteristics cannot be initialized until a Species has been selected");

        // TODO: make these depend on the selected species
        Raise(new CharacteristicValueSet(CharacteristicType.WeaponSkill, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.BallisticSkill, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Strength, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Toughness, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Initiative, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Agility, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Dexterity, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Intelligence, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Willpower, 30));
        Raise(new CharacteristicValueSet(CharacteristicType.Fellowship, 30));
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

            case CharacteristicValueSet characteristic:
                switch (characteristic.Characteristic)
                {
                    case CharacteristicType.WeaponSkill:
                        WeaponSkill = new CharacteristicState(characteristic.Value, WeaponSkill.Advances);
                        break;
                    case CharacteristicType.BallisticSkill:
                        BallisticSkill = new CharacteristicState(characteristic.Value, BallisticSkill.Advances);
                        break;
                    case CharacteristicType.Strength:
                        Strength = new CharacteristicState(characteristic.Value, Strength.Advances);
                        break;
                    case CharacteristicType.Toughness:
                        Toughness = new CharacteristicState(characteristic.Value, Toughness.Advances);
                        break;
                    case CharacteristicType.Initiative:
                        Initiative = new CharacteristicState(characteristic.Value, Initiative.Advances);
                        break;
                    case CharacteristicType.Agility:
                        Agility = new CharacteristicState(characteristic.Value, Agility.Advances);
                        break;
                    case CharacteristicType.Dexterity:
                        Dexterity = new CharacteristicState(characteristic.Value, Dexterity.Advances);
                        break;
                    case CharacteristicType.Intelligence:
                        Intelligence = new CharacteristicState(characteristic.Value, Intelligence.Advances);
                        break;
                    case CharacteristicType.Willpower:
                        Willpower = new CharacteristicState(characteristic.Value, Willpower.Advances);
                        break;
                    case CharacteristicType.Fellowship:
                        Fellowship = new CharacteristicState(characteristic.Value, Fellowship.Advances);
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown characteristic type [{characteristic.Characteristic}]");
                }
                ;

                break;

            case CharacteristicAdvanced advance:
                GetCharacteristic(advance.Characteristic).Advance();

                break;
        }
    }
}
