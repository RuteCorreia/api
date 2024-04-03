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

    public async Task<ExecutorViewModel> GetByIdAsync(string id)
    {
        var obj = await _executorRepository.GetByIdAsync(id);
        return _mapper.Map<ExecutorViewModel>(obj);
    }
}