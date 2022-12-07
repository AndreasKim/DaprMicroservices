using Ardalis.Specification.EntityFrameworkCore;
using Core.Application.Common.Interfaces;

namespace Core.Infrastructure.Persistence;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
  public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
  {
  }
}
