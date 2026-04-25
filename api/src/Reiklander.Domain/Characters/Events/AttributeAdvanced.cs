using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record AttributeAdvanced(AttributeType Attribute, int ExperienceCost) : IDomainEvent { }
