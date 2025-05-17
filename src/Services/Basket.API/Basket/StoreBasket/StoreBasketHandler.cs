
using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(bool IsSuccess);

public class StoreBasketCommandHandler(
    // DiscountProtoService.DiscountProtoServiceClient discountProto,
    IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        // await DeductDiscount(request, cancellationToken);

        await basketRepository.StoreBasket(request.Cart, cancellationToken);

        return new StoreBasketResult(true);
    }

    // private async Task DeductDiscount(StoreBasketCommand request, CancellationToken cancellationToken)
    // {
    //     foreach (var item in request.Cart.Items)
    //     {
    //         var coupon = await discountProto.GetDiscountAsync(
    //             new GetDiscountRequest { ProductName = item.ProductName },
    //             cancellationToken: cancellationToken);

    //         item.Price -= coupon.Amount;
    //     }
    // }
}
