using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class Repository<T>(CarServiceDbContext context) : IRepository<T> where T : class
{
    public virtual async Task<T?> GetById(object id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await context.Set<T>().ToListAsync();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> Delete(object id)
    {
        T? entity = await context.Set<T>().FindAsync(id);
        if (entity == null) return false;
        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> Update(T entity, object id)
    {
        if (await context.Set<T>().FindAsync(id) == null) return false;
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync() > 0;
    }
}