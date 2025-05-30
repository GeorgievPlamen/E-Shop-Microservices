
using Basket.API.Data;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<bool>;

public class CheckoutBasketHandler(
    IBasketRepository basketRepository,
    IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, bool>
{
    public async Task<bool> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        ArgumentNullException.ThrowIfNull(basket);

        var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);

        return true;
    }
}

