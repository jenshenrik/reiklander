using Reiklander.Api.Endpoints.Characters.Contracts;
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

        return new CharacterResponse
        (
            c.Name,
            c.Experience
        );
    }
}
