using Reiklander.Domain.Species;

namespace Reiklander.Application.Kernel;

public interface ISpeciesRepository
{
    Task<Species> GetAsync(string speciesName);
}
