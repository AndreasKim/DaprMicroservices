using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Core.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator) 
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<SalesInfo> SalesInfos => Set<SalesInfo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
