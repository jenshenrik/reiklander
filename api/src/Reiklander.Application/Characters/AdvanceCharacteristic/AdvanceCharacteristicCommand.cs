

using Reiklander.Domain.Characters;
using Reiklander.Domain.Characters.Characteristics;

namespace Reiklander.Application.Characters.AdvanceCharacteristic;

public record AdvanceCharacteristicCommand(CharacterId CharacterId, CharacteristicType Characteristic);

