using Reiklander.Application.Kernel;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure.Projections;

internal sealed record EventEnvelope<TPrimitive>(
        IDomainEvent Event,
        IAggregateId<TPrimitive> AggregateId,
        int Version,
        DateTime OccurredOn
) : IEventEnvelope<TPrimitive>
    where TPrimitive : notnull;
