using System.Linq.Expressions;

namespace ECommerceCart.Core.Domain.RepositoryContracts;

public interface IRepository<T>
    where T : class
{
    Task<T> Add(T entity);

    Task<List<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includeChains);
    Task<List<T>> GetAll(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includeChains
    );
    Task<T?> GetByQuery(Expression<Func<T, bool>> predicate);
    Task<T?> GetByQuery(params Func<IQueryable<T>, IQueryable<T>>[] includeChains);
    Task<T?> GetByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includeChains
    );

    Task<T> Update(T entity);
    Task<bool> Delete(Guid id);

    IQueryable<T> GetIQueryable();
}
