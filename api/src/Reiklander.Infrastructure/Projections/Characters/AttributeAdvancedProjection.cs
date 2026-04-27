using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Attributes;
using Reiklander.Domain.Characters.Events;
using Reiklander.Infrastructure.Persistence;
using Reiklander.Infrastructure.Queries.Characters.ReadModels;

namespace Reiklander.Infrastructure.Projections.Characters;

public class AttributeAdvancedProjection(EventStoreDbContext context) : IProjectionHandler<AttributeAdvanced>
{
    private readonly EventStoreDbContext context = context;

    public async Task Handle(AttributeAdvanced @event, Guid aggregateId)
    {
        var character = await context.Characters.FindAsync(aggregateId);

        if (character == null)
            return;

        var attribute = GetAttribute(character, @event.Attribute);

        var state = AttributeState.From(attribute.Value, attribute.Advances);
        var updatedAttributeState = state.Advance();

        attribute.Value = updatedAttributeState.Value;
        attribute.Bonus = updatedAttributeState.Bonus;
        attribute.CostToAdvance = updatedAttributeState.GetAdvanceCost();
        attribute.Advances = updatedAttributeState.Advances;
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
