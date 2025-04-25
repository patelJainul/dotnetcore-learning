using System.Linq.Expressions;
using ECommerceCart.Core.Domain.RepositoryContracts;
using ECommerceCart.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCart.Infrastructure.Repositories;

public class Repository<T>(ApplicationDbContext _db) : IRepository<T>
    where T : class
{
    public async Task<T> Add(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> GetAll()
    {
        return await _db.Set<T>().ToListAsync();
    }

    public IQueryable<T> GetIQueryable()
    {
        return _db.Set<T>().AsQueryable();
    }

    public async Task<T?> GetByQuery(Guid id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<T> Update(T entity)
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity == null)
            return false;
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<T?> GetByQuery(Expression<Func<T, bool>> predicate)
    {
        return await _db.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includeChains
    )
    {
        IQueryable<T> query = GetIQueryable().Where(predicate);
        foreach (var includeChain in includeChains)
        {
            query = includeChain(query);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> GetByQuery(params Func<IQueryable<T>, IQueryable<T>>[] includeChains)
    {
        IQueryable<T> query = GetIQueryable();
        foreach (var includeChain in includeChains)
        {
            query = includeChain(query);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
    {
        return await _db.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<List<T>> GetAll(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includeChains
    )
    {
        IQueryable<T> query = GetIQueryable().Where(predicate);
        foreach (var includeChain in includeChains)
        {
            query = includeChain(query);
        }
        return await query.ToListAsync();
    }

    public async Task<List<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includeChains)
    {
        IQueryable<T> query = GetIQueryable();
        foreach (var includeChain in includeChains)
        {
            query = includeChain(query);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _db.Set<T>().FindAsync(id);
    }
}
