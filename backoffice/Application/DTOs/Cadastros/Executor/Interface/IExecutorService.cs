using Application.DTOs.Cadastros.Executor.ViewModel;

namespace Application.DTOs.Cadastros.Executor.Interface;

public interface IExecutorService 
{
    Task<IEnumerable<ExecutorViewModel>> GetAllAsync(string? idEmpresa);

    Task<ExecutorViewModel?> GetByIdAsync(string id, string? idEmpresa);
}
