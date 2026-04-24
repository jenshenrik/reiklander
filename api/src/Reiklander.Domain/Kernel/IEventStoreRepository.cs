namespace Reiklander.Domain.Kernel;

public interface IEventStoreRepository
{
    Task<T> LoadAsync<T>(Guid id) where T : AggregateRoot, new();
    Task SaveAsync(AggregateRoot aggregate);
}
