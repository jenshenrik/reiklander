using Reiklander.Domain.Characters;

namespace Reiklander.Application.Characters.EarnExperiencePoints;

public record EarnExperiencePointsCommand(CharacterId CharacterId, int Amount);
