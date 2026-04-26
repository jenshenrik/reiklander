using Reiklander.Application.Kernel;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure.Projections;

public class ProjectionDispatcher(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task Dispatch(IDomainEvent e, Guid aggregateId)
    {
        var handlerType = typeof(IProjectionHandler<>)
            .MakeGenericType(e.GetType());

        var handler = serviceProvider.GetService(handlerType);

        if (handler == null) return;

        var method = handlerType.GetMethod("Handle");

        await (Task)method!.Invoke(handler, new object[] { e, aggregateId });
    }
}
