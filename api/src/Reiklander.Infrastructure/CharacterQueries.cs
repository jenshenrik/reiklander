using Reiklander.Api.Endpoints.Characters.Contracts;
using Reiklander.Contracts.Characters;
using Reiklander.Contracts.Attributes;
using Reiklander.Application;

namespace Reiklander.Infrastructure;

public class CharacterQueries(EventStoreDbContext context) : ICharacterQueries
{
    private readonly EventStoreDbContext context = context;

    public async Task<CharacterResponse> GetById(Guid id)
    {
        var c = await context.Characters.FindAsync(id);

        if (c == null)
            return null;

        var ws = new AttributeResponse(c.WeaponSkill.Value, c.WeaponSkill.Bonus, c.WeaponSkill.CostToAdvance);
        var bs = new AttributeResponse(c.BallisticSkill.Value, c.BallisticSkill.Bonus, c.BallisticSkill.CostToAdvance);
        var s = new AttributeResponse(c.Strength.Value, c.Strength.Bonus, c.Strength.CostToAdvance);
        var t = new AttributeResponse(c.Toughness.Value, c.Toughness.Bonus, c.Toughness.CostToAdvance);
        var i = new AttributeResponse(c.Initiative.Value, c.Initiative.Bonus, c.Initiative.CostToAdvance);
        var agi = new AttributeResponse(c.Agility.Value, c.Agility.Bonus, c.Agility.CostToAdvance);
        var dex = new AttributeResponse(c.Dexterity.Value, c.Dexterity.Bonus, c.Dexterity.CostToAdvance);
        var intel = new AttributeResponse(c.Intelligence.Value, c.Intelligence.Bonus, c.Intelligence.CostToAdvance);
        var wis = new AttributeResponse(c.Willpower.Value, c.Willpower.Bonus, c.Willpower.CostToAdvance);
        var fel = new AttributeResponse(c.Fellowship.Value, c.Fellowship.Bonus, c.Fellowship.CostToAdvance);
        return new CharacterResponse
        (
            c.Name,
            c.Experience,
            ws, bs, s, t, i, agi, dex, intel, wis, fel
        );
    }
}
