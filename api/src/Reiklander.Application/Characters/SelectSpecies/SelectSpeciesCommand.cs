namespace Reiklander.Application.Characters.SelectSpecies;

public record SelectSpeciesCommand(Guid CharacterId, string SpeciesIdentifier);
