using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Models;

public class Prodct : Entity<ProductId>
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
}