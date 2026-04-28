using Reiklander.Application;
using Reiklander.Infrastructure.Persistence;
using Reiklander.Contracts.Characters;
using Reiklander.Contracts.Characteristics;

namespace Reiklander.Infrastructure.Queries.Characters;

public class CharacterQueries(EventStoreDbContext context) : ICharacterQueries
{
    private readonly EventStoreDbContext context = context;

    public async Task<CharacterResponse> GetById(Guid id)
    {
        var c = await context.Characters.FindAsync(id);

        if (c == null)
            return null;

        var ws = new CharacteristicResponse(c.WeaponSkill.Value, c.WeaponSkill.Bonus, c.WeaponSkill.CostToAdvance);
        var bs = new CharacteristicResponse(c.BallisticSkill.Value, c.BallisticSkill.Bonus, c.BallisticSkill.CostToAdvance);
        var s = new CharacteristicResponse(c.Strength.Value, c.Strength.Bonus, c.Strength.CostToAdvance);
        var t = new CharacteristicResponse(c.Toughness.Value, c.Toughness.Bonus, c.Toughness.CostToAdvance);
        var i = new CharacteristicResponse(c.Initiative.Value, c.Initiative.Bonus, c.Initiative.CostToAdvance);
        var agi = new CharacteristicResponse(c.Agility.Value, c.Agility.Bonus, c.Agility.CostToAdvance);
        var dex = new CharacteristicResponse(c.Dexterity.Value, c.Dexterity.Bonus, c.Dexterity.CostToAdvance);
        var intel = new CharacteristicResponse(c.Intelligence.Value, c.Intelligence.Bonus, c.Intelligence.CostToAdvance);
        var wis = new CharacteristicResponse(c.Willpower.Value, c.Willpower.Bonus, c.Willpower.CostToAdvance);
        var fel = new CharacteristicResponse(c.Fellowship.Value, c.Fellowship.Bonus, c.Fellowship.CostToAdvance);
        return new CharacterResponse
        (
            c.Species,
            c.Name,
            c.Experience,
            c.ExperienceSpent,
            c.ExperienceTotal,
            ws, bs, s, t, i, agi, dex, intel, wis, fel
        );
    }
}
