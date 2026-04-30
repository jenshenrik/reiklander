namespace Reiklander.Domain.Kernel;

public abstract class AggregateRoot<TId, TPrimitive> : AggregateRoot
    where TId : struct, IAggregateId<TId, TPrimitive>
    where TPrimitive : notnull
{
    public TId Id { get; protected set; }

    public override object IdValue => Id.Value;
}

public abstract class AggregateRoot
{
    public abstract object IdValue { get; }

    public int Version { get; private set; }

    private readonly List<IDomainEvent> _uncommittedEvents = [];
    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents;

    protected void Raise(IDomainEvent e)
    {
        Apply(e);
        _uncommittedEvents.Add(e);
    }

    protected abstract void Apply(IDomainEvent e);

    public void MarkEventsCommitted()
    {
        _uncommittedEvents.Clear();
    }

    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        foreach (var e in events)
        {
            Apply(e);
            Version++;
        }
    }
}


