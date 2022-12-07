using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations;

public class SalesInfoConfiguration : IEntityTypeConfiguration<SalesInfo>
{
    public void Configure(EntityTypeBuilder<SalesInfo> builder)
    {
        var valueComparer = new ValueComparer<int[]>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToArray());

        builder
            .Property(e => e.RatingScore)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(p => Convert.ToInt32(p)).ToArray()).Metadata.SetValueComparer(valueComparer);
    }
}
