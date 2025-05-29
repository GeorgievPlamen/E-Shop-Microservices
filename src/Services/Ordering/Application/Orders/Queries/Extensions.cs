using Application.Dtos;
using Domain.Models;
using Domain.ValueObjects;

namespace Application.Orders.Queries;

public static class Extensions
{
    internal static List<OrderDto> ProjectToOrderDto(this IEnumerable<Order> orders)
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