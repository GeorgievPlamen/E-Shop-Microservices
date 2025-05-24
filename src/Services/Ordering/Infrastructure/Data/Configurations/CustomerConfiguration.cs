using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Email)
            .HasMaxLength(100);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                customerId => customerId.Value,
                dbId => new CustomerId(dbId));
    }
}
