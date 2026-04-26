using Reiklander.Application.Kernel;
using Reiklander.Domain.Species;

namespace Reiklander.Infrastructure.Persistence;

public class SpeciesRepository : ISpeciesRepository
{
    public Task<Species> GetAsync(string speciesName)
    {
        return Task.FromResult(new Species("Human (Reiklander)"));
    }
}
