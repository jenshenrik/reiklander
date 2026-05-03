using Reiklander.Domain.Kernel;

namespace Reiklander.Application.Kernel;

public interface IEventEnvelope<out TPrimitive>
    where TPrimitive : notnull
{
    IDomainEvent Event { get; }
    IAggregateId<TPrimitive> AggregateId { get; }
    int Version { get; }
    DateTime OccurredOn { get; }
}
