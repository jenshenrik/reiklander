using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public class Character : AggregateRoot
{
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
        }
    }
}
