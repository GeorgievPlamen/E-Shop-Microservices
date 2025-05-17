using Discount.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data;

public class DiscountContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 200 });
    }
}