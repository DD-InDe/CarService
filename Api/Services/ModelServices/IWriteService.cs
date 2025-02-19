namespace Api.Services.ModelServices;

public interface IWriteService<TDto, TModel, in TViewModel> : IReadService<TDto, TModel>
{
    Task<bool> AddObject(TViewModel newObject);
    Task<bool> UpdateObject(TViewModel viewModel);
    Task<bool> DeleteObject(int id);
    
    TModel ToModel(TViewModel viewModel);
}