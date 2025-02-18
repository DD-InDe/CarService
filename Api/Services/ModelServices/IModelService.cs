namespace Api.Services.ModelServices;

public interface IModelService<D, M, VM>
{
    Task<D?> GetObjectById(int id);
    Task<List<D>> GetAllObjects();
    Task<bool> AddObject(VM newObject);
    Task<bool> UpdateObject(VM viewModel);
    Task<bool> DeleteObject(int id);
    D ToDto(M model);
    M ToModel(VM dto);
}