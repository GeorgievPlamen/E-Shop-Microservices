namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string Color { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
}