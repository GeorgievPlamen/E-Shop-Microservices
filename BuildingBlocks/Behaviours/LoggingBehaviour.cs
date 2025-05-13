using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>
    (ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling request: {request}", request);
        var response = await next(cancellationToken);

        logger.LogInformation("Response: {response}", response);
        return response;
    }
}