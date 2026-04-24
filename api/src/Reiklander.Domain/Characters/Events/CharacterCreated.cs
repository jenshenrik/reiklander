using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record CharacterCreated(string Name) : IDomainEvent { }
