using Core.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(p => p.Street).IsRequired();
        builder.Property(p => p.StreetNo).IsRequired();
        builder.Property(p => p.City).IsRequired();
        builder.Property(p => p.PLZ).IsRequired();
        builder.Property(p => p.Telephone).IsRequired();
        builder.Property(p => p.CorporateEmail).IsRequired();
    }
}
