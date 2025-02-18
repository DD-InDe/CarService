namespace Api.Repositories;

public interface IRepository<T>
{
    Task<T?> GetById(int id);
    Task<List<T>> GetAll();
    Task<bool> Add(T entity);
    Task<bool> Delete(int id);
    Task<bool> Update(T entity);
}