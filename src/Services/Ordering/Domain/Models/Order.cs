using Domain.Abstractions;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; }
    public string OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = null!;
    public Address BillingAddress { get; private set; } = null!;
    public Payment Payment { get; private set; } = null!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice => OrderItems.Sum(x => x.Price * x.Quantity);
}