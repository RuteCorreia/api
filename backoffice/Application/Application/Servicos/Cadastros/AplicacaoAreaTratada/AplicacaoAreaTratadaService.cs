using Application.DTOs.Cadastros.AplicacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;

namespace Application.Application.Servicos.Cadastros.AplicacaoAreaTratada;

public class AplicacaoAreaTratadaService : IAplicacaoAreaTratadaService
{
    private readonly IAplicacaoAreaTratadaRepository _aplicacaoAreaTratadaRepository;
    private readonly IMapper _mapper;

    public AplicacaoAreaTratadaService(IMapper mapper, IAplicacaoAreaTratadaRepository aplicacaoAreaTratadaRepository)
    {
        _aplicacaoAreaTratadaRepository = aplicacaoAreaTratadaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoAreaTratadaViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoAreaTratadaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoAreaTratadaViewModel>>(list);
    }

    public async Task<AplicacaoAreaTratadaViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoAreaTratadaRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoAreaTratadaViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoAreaTratadaViewModel obj)
    {
        var mapAplicacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>(obj);
        await _aplicacaoAreaTratadaRepository.AddAsync(mapAplicacaoAreaTratada);
    }

    public async Task UpdateAsync(AplicacaoAreaTratadaViewModel obj)
    {
        var mapAplicacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>(obj);
        await _aplicacaoAreaTratadaRepository.UpdateAsync(mapAplicacaoAreaTratada);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoAreaTratadaRepository.DeleteAsync(id);
    }
}
