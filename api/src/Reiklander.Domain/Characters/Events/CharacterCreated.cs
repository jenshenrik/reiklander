using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record CharacterCreated(Guid Id, string Name) : IDomainEvent { }
