using Application.DTOs.Cadastros.AplicacaoLog.Interface;
using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoLog;

namespace Application.Application.Servicos.Cadastros.AplicacaoLog;

public class AplicacaoLogService : IAplicacaoLogService
{
    private readonly IAplicacaoLogRepository _aplicacaoLogRepository;
    private readonly IMapper _mapper;

    public AplicacaoLogService(IMapper mapper, IAplicacaoLogRepository aplicacaoLogRepository)
    {
        _aplicacaoLogRepository = aplicacaoLogRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoLogViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoLogRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoLogViewModel>>(list);
    }

    public async Task<AplicacaoLogViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoLogRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoLogViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoLogViewModel obj)
    {
        var mapAplicacaoLog = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>(obj);
        await _aplicacaoLogRepository.AddAsync(mapAplicacaoLog);
    }

    public async Task UpdateAsync(AplicacaoLogViewModel obj)
    {
        var mapAplicacaoLog = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>(obj);
        await _aplicacaoLogRepository.UpdateAsync(mapAplicacaoLog);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoLogRepository.DeleteAsync(id);
    }
}
