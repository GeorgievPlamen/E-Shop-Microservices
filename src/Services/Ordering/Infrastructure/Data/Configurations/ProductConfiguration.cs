using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                customerId => customerId.Value,
                dbId => new ProductId(dbId));
    }
}
