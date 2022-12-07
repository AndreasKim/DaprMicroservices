using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.Property(p => p.ShopOwnerId).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Rating).HasMaxLength(1).IsRequired();
        builder.Property(p => p.Street).IsRequired();
        builder.Property(p => p.StreetNo).IsRequired();
        builder.Property(p => p.City).IsRequired();
        builder.Property(p => p.PLZ).IsRequired();
        builder.Property(p => p.CorporateEmail).IsRequired();
    }
}
