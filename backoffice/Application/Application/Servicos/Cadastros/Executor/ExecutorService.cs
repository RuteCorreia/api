using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Executor.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Executor;

namespace Application.Application.Servicos.Cadastros.Executor;

public class ExecutorService : IExecutorService
{
    private readonly IExecutorRepository _executorRepository;
    private readonly IMapper _mapper;

    public ExecutorService(IMapper mapper, IExecutorRepository executorRepository)
    {
        _executorRepository = executorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExecutorViewModel>> GetAllAsync()
    {
        var list = await _executorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExecutorViewModel>>(list);
    }

    public async Task<ExecutorViewModel> GetByIdAsync(int id)
    {
        var obj = await _executorRepository.GetByIdAsync(id);
        return _mapper.Map<ExecutorViewModel>(obj);
    }

    public async Task<ExecutorViewModel> GetByLoginAsync(string email, string password)
    {
        var obj = await _executorRepository.GetByLoginAsync(email, password);
        return _mapper.Map<ExecutorViewModel>(obj);
    }

    public async Task AddAsync(ExecutorViewModel obj)
    {
        var mapExecutor = _mapper.Map<Domain.Entidades.Cadastros.Executor.Executor>(obj);
        await _executorRepository.AddAsync(mapExecutor);
    }

    public async Task UpdateAsync(ExecutorViewModel obj)
    {
        var mapExecutor = _mapper.Map<Domain.Entidades.Cadastros.Executor.Executor>(obj);
        await _executorRepository.UpdateAsync(mapExecutor);
    }

    public async Task DeleteAsync(int id)
    {
        await _executorRepository.DeleteAsync(id);
    }
}
