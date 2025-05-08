using System.Linq.Expressions;
using ECommerceCartCrud.Core.Domain.RepositoryContracts;
using ECommerceCartCrud.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCartCrud.Infrastructure.Repository;

public class Repository<T>(ApplicationDbContext db) : IRepository<T>
    where T : class
{
    public async Task<T?> Add(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await db.Set<T>().FindAsync(id);
        if (entity is null)
        {
            return false;
        }
        db.Set<T>().Remove(entity);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<List<T>> GetAll()
    {
        return await db.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    )
    {
        IQueryable<T> query = db.Set<T>().Where(predicate).AsQueryable();
        foreach (var include in includes)
        {
            query = include(query);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetById(Guid Id)
    {
        return await db.Set<T>().FindAsync(Id);
    }

    public async Task<T?> GetByQuery(
        Expression<Func<T, bool>> predicate,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    )
    {
        var query = db.Set<T>().Where(predicate).AsQueryable();
        foreach (var include in includes)
        {
            query = include(query);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> Update(T entity)
    {
        db.Set<T>().Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}
