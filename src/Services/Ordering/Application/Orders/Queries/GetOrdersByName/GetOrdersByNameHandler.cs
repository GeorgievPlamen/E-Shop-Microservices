using Application.Data;
using Application.Dtos;
using BuildingBlocks.CQRS;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(request.Name))
            .OrderBy(x => x.OrderName)
            .ToListAsync(cancellationToken: cancellationToken);

        var orderDtos = ProjectToOrderDto(orders);

        return new GetOrdersByNameResult(orderDtos);
    }

    private List<OrderDto> ProjectToOrderDto(List<Order> orders)
    {
        List<OrderDto> result = [];

        foreach (var order in orders)
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
                new CustomerId(order.CustomerId.Value),
                new OrderName(order.OrderName.Value),
                shippingAddress,
                billingAddress,
                payment,
                order.Status);

            foreach (var orderItemDto in order.OrderItems)
            {
                newOrder.Add(
                    new ProductId(orderItemDto.ProductId.Value),
                    orderItemDto.Quantity,
                    orderItemDto.Price);
            }
        }

        return result;
    }
}
