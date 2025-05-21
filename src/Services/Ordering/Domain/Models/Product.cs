using Domain.Abstractions;

namespace Domain.Models;

public class Prodct : Entity<Guid>
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
}