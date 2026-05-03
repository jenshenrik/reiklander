using Reiklander.Application.Kernel;

namespace Reiklander.Infrastructure.Projections;

public class ProjectionDispatcher(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task Dispatch<TPrimitive>(IEventEnvelope<TPrimitive> envelope)
        where TPrimitive : notnull
    {
        var handlerType = typeof(IProjectionHandler<,>)
            .MakeGenericType(envelope.Event.GetType(), typeof(TPrimitive));

        var handler = serviceProvider.GetService(handlerType);

        if (handler == null) return;

        var method = handlerType.GetMethod("Handle");

        await (Task)method!.Invoke(handler, [envelope.Event, envelope]);
    }
}
