namespace Reiklander.Domain.Kernel;

public abstract class AggregateRoot<TId, TPrimitive>
    where TId : struct, IAggregateId<TId, TPrimitive>
    where TPrimitive : notnull
{
    public TId Id { get; protected set; }
    public int Version { get; private set; }

    private readonly List<IDomainEvent> _uncommittedEvents = [];
    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents;

    protected abstract void Apply(IDomainEvent e);

    protected void Raise(IDomainEvent e)
    {
        Apply(e);
        _uncommittedEvents.Add(e);
    }

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
