namespace Api.Services.ModelServices;

public interface IWriteService<TDto, TModel, in TViewModel> : IReadService<TDto, TModel, TViewModel>
{
    Task<bool> AddObject(TViewModel newObject);
    Task<bool> UpdateObject(TViewModel viewModel);
    Task<bool> DeleteObject(int id);
}