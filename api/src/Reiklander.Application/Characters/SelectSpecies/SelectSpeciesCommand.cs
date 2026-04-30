using Reiklander.Domain.Characters;

namespace Reiklander.Application.Characters.SelectSpecies;

public record SelectSpeciesCommand(CharacterId CharacterId, string SpeciesIdentifier);
