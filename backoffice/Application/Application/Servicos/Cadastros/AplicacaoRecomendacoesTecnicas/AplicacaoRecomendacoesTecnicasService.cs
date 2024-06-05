using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;

namespace Application.Application.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas;

public class AplicacaoRecomendacoesTecnicasService : IAplicacaoRecomendacoesTecnicasService
{
    private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;
    private readonly IMapper _mapper;

    public AplicacaoRecomendacoesTecnicasService(IMapper mapper, IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository)
    {
        _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRecomendacoesTecnicasRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>>(list);
    }

    public async Task<AplicacaoRecomendacoesTecnicasViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRecomendacoesTecnicasRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoRecomendacoesTecnicasViewModel>(obj);
    }

    public async Task<int> AddAsync(AplicacaoRecomendacoesTecnicasViewModel obj)
    {
        var mapAplicacaoRecomendacoesTecnicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>(obj);
        var aplicacaoRecomendacoesTecnicas = _aplicacaoRecomendacoesTecnicasRepository.AddAsync(mapAplicacaoRecomendacoesTecnicas);
        return aplicacaoRecomendacoesTecnicas.Result;
    }

    public async Task UpdateAsync(AplicacaoRecomendacoesTecnicasViewModel obj)
    {
        var mapAplicacaoRecomendacoesTecnicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>(obj);
        await _aplicacaoRecomendacoesTecnicasRepository.UpdateAsync(mapAplicacaoRecomendacoesTecnicas);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRecomendacoesTecnicasRepository.DeleteAsync(id);
    }
}
