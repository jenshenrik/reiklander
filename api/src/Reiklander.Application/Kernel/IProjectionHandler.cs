namespace Reiklander.Application.Kernel;

public interface IProjectionHandler<in TEvent, TPrimitive>
    where TPrimitive : notnull
{
    Task Handle(TEvent @event, IEventEnvelope<TPrimitive> envelope);
}
