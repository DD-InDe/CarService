namespace Api.Services.ModelServices;

public interface IModelService<D, O>
{
    Task<D> GetObjectById(int id);
    Task<List<D>> GetAllObjects();
    Task<bool> AddObject(D newObject);
    Task<bool> UpdateObject(D newObject);
    Task<bool> DeleteObject(int id);
    D ToDto(O model);
    O FromDto(D dto);
}