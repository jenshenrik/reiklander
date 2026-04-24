using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record ExperienceEarned(int Amount) : IDomainEvent { }


