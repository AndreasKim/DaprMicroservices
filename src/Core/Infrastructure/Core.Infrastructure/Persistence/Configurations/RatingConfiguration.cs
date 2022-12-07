using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.Property(p => p.RatingValue).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.UserFullName).IsRequired();
    }
}
