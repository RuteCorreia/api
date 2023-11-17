using Application.DTOs.Cadastros.AplicacaoCaracteristicas.Interface;
using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;

namespace Application.Application.Servicos.Cadastros.AplicacaoCaracteristicas;

public class AplicacaoCaracteristicasService : IAplicacaoCaracteristicasService
{
    private readonly IAplicacaoCaracteristicasRepository _aplicacaoCaracteristicasRepository;
    private readonly IMapper _mapper;

    public AplicacaoCaracteristicasService(IMapper mapper, IAplicacaoCaracteristicasRepository aplicacaoCaracteristicasRepository)
    {
        _aplicacaoCaracteristicasRepository = aplicacaoCaracteristicasRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoCaracteristicasViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoCaracteristicasRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoCaracteristicasViewModel>>(list);
    }

    public async Task<AplicacaoCaracteristicasViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoCaracteristicasRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoCaracteristicasViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoCaracteristicasViewModel obj)
    {
        var mapAplicacaoCaracteristicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>(obj);
        await _aplicacaoCaracteristicasRepository.AddAsync(mapAplicacaoCaracteristicas);
    }

    public async Task UpdateAsync(AplicacaoCaracteristicasViewModel obj)
    {
        var mapAplicacaoCaracteristicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>(obj);
        await _aplicacaoCaracteristicasRepository.UpdateAsync(mapAplicacaoCaracteristicas);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoCaracteristicasRepository.DeleteAsync(id);
    }
}
