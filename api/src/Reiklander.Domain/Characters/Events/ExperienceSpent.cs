using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record ExperienceSpent(int Amount, string? Source) : IDomainEvent { };
