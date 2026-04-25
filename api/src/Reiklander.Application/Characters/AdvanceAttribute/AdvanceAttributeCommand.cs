using Reiklander.Domain.Characters;

namespace Reiklander.Application.Characters.AdvanceAttribute;

public record AdvanceAttributeCommand(Guid CharacterId, AttributeType Attribute);
