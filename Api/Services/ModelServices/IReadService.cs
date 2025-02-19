namespace Api.Services.ModelServices;

public interface IReadService<TDto, TModel>
{
    Task<TDto?> GetObjectById(int id);
    Task<List<TDto>> GetAllObjects();
    TDto ToDto(TModel model);
}