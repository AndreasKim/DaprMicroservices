using Ardalis.Specification;

namespace Core.Application.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
