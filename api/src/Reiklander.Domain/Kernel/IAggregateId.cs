namespace Reiklander.Domain.Kernel;

public interface IAggregateId<out TPrimitive>
    where TPrimitive : notnull
{
    TPrimitive Value { get; }
}

public interface IAggregateId<TSelf, TPrimitive> : IAggregateId<TPrimitive>
    where TSelf : struct, IAggregateId<TSelf, TPrimitive>
    where TPrimitive : notnull
{
    static abstract TSelf From(TPrimitive value);
}
