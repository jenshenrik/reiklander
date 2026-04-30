using Reiklander.Domain.Characters;

namespace Reiklander.Application.Characters.NameCharacter;

public record NameCharacterCommand(CharacterId CharacterId, string Name);
