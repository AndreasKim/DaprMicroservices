using Ardalis.Specification.EntityFrameworkCore;
using Core.Application.Interfaces;
using System.Diagnostics;

namespace Services.ProductsService.Infrastructure.Persistence;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public EfRepository(ApplicationDbContext dbContext, ActivitySource daprActivity) : base(dbContext)
    {
        using var activity = daprActivity.StartActivity($"{nameof(EfRepository<T>)}/AccessRepository");
    }
}
