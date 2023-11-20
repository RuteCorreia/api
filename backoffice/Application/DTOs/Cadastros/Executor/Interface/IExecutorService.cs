using Application.DTOs.Cadastros.Executor.ViewModel;

namespace Application.DTOs.Cadastros.Executor.Interface;

public interface IExecutorService 
{
    Task<IEnumerable<ExecutorViewModel>> GetAllAsync();

    Task<ExecutorViewModel> GetByIdAsync(int id);

    Task AddAsync(ExecutorViewModel obj);

    Task UpdateAsync(ExecutorViewModel obj);

    Task DeleteAsync(int id);
}
