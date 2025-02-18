namespace Api.Repositories;

public interface IRepository<T>
{
    Task<T?> GetById(object id);
    Task<List<T>> GetAll();
    Task<bool> Add(T entity);
    Task<bool> Delete(object id);
    Task<bool> Update(T entity, object id);
}