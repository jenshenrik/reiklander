using Reiklander.Domain.Characters.Attributes;

namespace Reiklander.Application.Characters.AdvanceAttribute;

public record AdvanceAttributeCommand(Guid CharacterId, AttributeType Attribute);
