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

        character.Raise(new CharacterCreated(name));

        return character;
    }

    public string Name { get; private set; } = default!;

    public int ExperiencePoints { get; private set; }

    public void EarnExperience(int amount)
    {
        Raise(new ExperienceEarned(amount));
    }

    protected override void Apply(IDomainEvent e)
    {
        switch (e)
        {
            case ExperienceEarned ee:
                ExperiencePoints += ee.Amount;

                break;

            case CharacterCreated cc:
                Name = cc.Name;

                break;
        }
    }
}
