using Reiklander.Domain.Characters;

namespace Reiklander.Domain.Kernel;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }

    public int Version { get; private set; }

    private readonly List<IDomainEvent> _uncommittedEvents = new();
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


