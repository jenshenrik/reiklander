using Reiklander.Domain.Characters.Characteristics;
using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record CharacteristicAdvanced(CharacteristicType Characteristic) : IDomainEvent { }
