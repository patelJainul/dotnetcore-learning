using System.Linq.Expressions;

namespace ECommerceCartCrud.Core.Domain.RepositoryContracts;

public interface IRepository<T>
    where T : class
{
    Task<T?> Add(T entity);
    Task<T?> GetById(Guid Id);
    Task<List<T>> GetAll();
    Task<T?> GetByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );
    Task<List<T>> GetAllByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );
    Task<T?> Update(T entity);
    Task<bool> Delete(Guid id);
}
