namespace Reiklander.Domain.Kernel;

public interface IEventStoreRepository
{
    Task<TAggregate> LoadAsync<TAggregate, TId, TPrimitive>(TId id)
        where TAggregate : AggregateRoot<TId, TPrimitive>, new()
        where TId : struct, IAggregateId<TId, TPrimitive>
        where TPrimitive : notnull;

    Task SaveAsync<TAggregate, TId, TPrimitive>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId, TPrimitive>
        where TId : struct, IAggregateId<TId, TPrimitive>
        where TPrimitive : notnull;
}
