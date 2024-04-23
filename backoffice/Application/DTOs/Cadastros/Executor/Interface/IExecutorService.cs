using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using Application.DTOs.Cadastros.Executor.ViewModel;

namespace Application.DTOs.Cadastros.Executor.Interface;

public interface IExecutorService 
{
    Task<IEnumerable<ExecutorViewModel>> GetAllAsync();

    Task<ExecutorViewModel?> GetByIdAsync(string id);
}
