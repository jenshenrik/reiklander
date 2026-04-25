using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters;
using Reiklander.Domain.Characters.Events;

namespace Reiklander.Infrastructure;

public class AttributeAdvancedProjection(EventStoreDbContext context) : IProjectionHandler<AttributeAdvanced>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(AttributeAdvanced @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null)
            return;

        var attribute = GetAttribute(character, @event.Attribute);

        attribute.Value++;
        attribute.Bonus = attribute.Value / 10;
        attribute.CostToAdvance = 0; // TODO: fix
        attribute.Advances++;
        character.Experience -= @event.ExperienceCost;
    }

    private static AttributeReadModel GetAttribute(CharacterReadModel character, AttributeType attribute) =>
         attribute switch
         {
             AttributeType.WeaponSkill => character.WeaponSkill,
             AttributeType.BallisticSkill => character.BallisticSkill,
             AttributeType.Strength => character.Strength,
             AttributeType.Toughness => character.Toughness,
             AttributeType.Initiative => character.Initiative,
             AttributeType.Agility => character.Agility,
             AttributeType.Dexterity => character.Dexterity,
             AttributeType.Intelligence => character.Intelligence,
             AttributeType.Willpower => character.Willpower,
             AttributeType.Fellowship => character.Fellowship,
             _ => throw new ArgumentOutOfRangeException(),
         };
}
