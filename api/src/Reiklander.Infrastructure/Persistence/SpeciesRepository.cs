using System.Collections.Generic;
using Reiklander.Application.Kernel;
using Reiklander.Domain.Species;

namespace Reiklander.Infrastructure.Persistence;

public class SpeciesRepository : ISpeciesRepository
{
    public Task<Species> GetAsync(string identifier)
    {
        return Task.FromResult(species.First(s => s.Identifier == identifier));
    }

    private List<Species> species = new List<Species>()
    {
        new("Human (Reiklander)", "human"),
        new("Halfling", "halfling"),
        new("Dwarf", "dwarf"),
        new("High Elf", "highelf"),
        new("Wood Elf", "woodelf"),
    };
}
