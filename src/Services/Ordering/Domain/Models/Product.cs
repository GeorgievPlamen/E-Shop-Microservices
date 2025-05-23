using System.Net.Sockets;
using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = null!;

    public decimal Price { get; private set; }
    public static Product Create(ProductId id, string name, decimal price) => new()
    {
        Id = id,
        Name = name,
        Price = price
    };
}