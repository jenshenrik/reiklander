

using Reiklander.Domain.Characters.Characteristics;

namespace Reiklander.Application.Characters.AdvanceCharacteristic;

public record AdvanceCharacteristicCommand(Guid CharacterId, CharacteristicType Characteristic);

