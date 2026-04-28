using Reiklander.Contracts.Characters;

namespace Reiklander.Application;

public interface ICharacterQueries
{
    Task<CharacterResponse> GetById(Guid id);
}
