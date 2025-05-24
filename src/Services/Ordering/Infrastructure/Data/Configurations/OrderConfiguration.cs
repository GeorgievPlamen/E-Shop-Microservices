using Domain.Enums;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                customerId => customerId.Value,
                dbId => new OrderId(dbId));

        builder.ComplexProperty(x => x.BillingAddress);
        builder.ComplexProperty(x => x.ShippingAddress);
        builder.ComplexProperty(x => x.Payment);
        builder.ComplexProperty(x => x.OrderName);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.Property(x => x.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(x => x.ToString(), dbStatus => Enum.Parse<OrderStatus>(dbStatus));
    }
}
