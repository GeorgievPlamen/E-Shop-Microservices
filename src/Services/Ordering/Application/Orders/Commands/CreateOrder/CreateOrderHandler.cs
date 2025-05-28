using Application.Data;
using Application.Dtos;
using BuildingBlocks.CQRS;
using Domain.Models;
using Domain.ValueObjects;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(request.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = new Address(
            order.ShippingAddress.FirstName,
            order.ShippingAddress.LastName,
            order.ShippingAddress.EmailAddress,
            order.ShippingAddress.AddressLine,
            order.ShippingAddress.Country,
            order.ShippingAddress.State,
            order.ShippingAddress.ZipCode);

        var billingAddress = new Address(
            order.BillingAddress.FirstName,
            order.BillingAddress.LastName,
            order.BillingAddress.EmailAddress,
            order.BillingAddress.AddressLine,
            order.BillingAddress.Country,
            order.BillingAddress.State,
            order.BillingAddress.ZipCode);

        var payment = new Payment(
                order.Payment.CardName,
                order.Payment.CardNumber,
                order.Payment.Expiration,
                order.Payment.CVV,
                order.Payment.PaymentMethod);

        var newOrder = Order.Create(
            new OrderId(Guid.NewGuid()),
            new CustomerId(order.CustomerId),
            new OrderName(order.OrderName),
            shippingAddress,
            billingAddress,
            payment,
            order.Status);

        foreach (var orderItemDto in order.OrderItems)
        {
            newOrder.Add(
                new ProductId(orderItemDto.ProductId),
                orderItemDto.Quantity,
                orderItemDto.Price);
        }

        return newOrder;
    }
}
