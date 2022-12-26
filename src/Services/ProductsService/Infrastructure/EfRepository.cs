using Ardalis.Specification.EntityFrameworkCore;
using Core.Application.Interfaces;

namespace Services.ProductsService.Infrastructure.Persistence;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
  public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
  {
  }
}
