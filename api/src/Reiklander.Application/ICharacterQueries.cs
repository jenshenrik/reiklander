using Reiklander.Api.Endpoints.Characters.Contracts;

namespace Reiklander.Application;

public interface ICharacterQueries
{
    Task<CharacterResponse> GetById(Guid id);
}
