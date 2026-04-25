namespace Reiklander.Application.Characters.EarnExperiencePoints;

public record EarnExperiencePointsCommand(Guid CharacterId, int Amount);
