namespace Reiklander.Application.Kernel;

public interface IProjectionHandler<in TEvent>
{
    Task Handle(TEvent @event, Guid aggregateId);
}
