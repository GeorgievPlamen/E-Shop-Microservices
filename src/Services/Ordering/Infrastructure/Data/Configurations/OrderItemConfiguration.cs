using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                customerId => customerId.Value,
                dbId => new OrderItemId(dbId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
