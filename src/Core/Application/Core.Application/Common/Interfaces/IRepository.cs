using Ardalis.Specification;

namespace Core.Application.Common.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
