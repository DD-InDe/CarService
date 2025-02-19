namespace Api.Services.ModelServices;

public interface IReadService<TDto, TModel, in TViewModel>
{
    Task<TDto?> GetObjectById(int id);
    Task<List<TDto>> GetAllObjects();
    TDto ToDto(TModel model);
    TModel ToModel(TViewModel viewModel);
}