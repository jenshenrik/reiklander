namespace Reiklander.Infrastructure;

public class EventEntity
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; } = default!;
    public int Version { get; set; }
    public string EventType { get; set; } = default!;
    public string Data { get; set; } = default!;
    public DateTime OccurredOn { get; set; }
}
