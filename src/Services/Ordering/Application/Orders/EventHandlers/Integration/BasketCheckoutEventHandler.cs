using Application.Orders.Commands.CreateOrder;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(
    ISender mediator,
    ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {Event}", context);

        var command = MapToCreateOrderCommand(context.Message);

        await mediator.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        return new CreateOrderCommand(default!); // fake
    }
}
