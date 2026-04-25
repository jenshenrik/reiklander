using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record NameCharacter(string Name) : IDomainEvent { }
