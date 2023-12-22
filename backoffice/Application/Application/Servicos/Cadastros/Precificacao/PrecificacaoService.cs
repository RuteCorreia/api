using Application.DTOs.Cadastros.Precificacao.Interface;
using Application.DTOs.Cadastros.Precificacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Precificacao;

namespace Application.Application.Servicos.Cadastros.Precificacao;

public class PrecificacaoService : IPrecificacaoService
{
    private readonly IPrecificacaoRepository _precificacaoRepository;
    private readonly IMapper _mapper;

    public PrecificacaoService(IMapper mapper, IPrecificacaoRepository precificacaoRepository)
    {
        _precificacaoRepository = precificacaoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PrecificacaoViewModel>> GetAllAsync()
    {
        var list = await _precificacaoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PrecificacaoViewModel>>(list);
    }

    public async Task<PrecificacaoViewModel> GetByIdAsync(int id)
    {
        var obj = await _precificacaoRepository.GetByIdAsync(id);
        return _mapper.Map<PrecificacaoViewModel>(obj);
    }

    public async Task AddAsync(PrecificacaoViewModel obj)
    {
        var mapPrecificacao = _mapper.Map<Domain.Entidades.Cadastros.Precificacao.Precificacao>(obj);
        await _precificacaoRepository.AddAsync(mapPrecificacao);
    }

    public async Task UpdateAsync(PrecificacaoViewModel obj)
    {
        var mapPrecificacao = _mapper.Map<Domain.Entidades.Cadastros.Precificacao.Precificacao>(obj);
        await _precificacaoRepository.UpdateAsync(mapPrecificacao);
    }

    public async Task DeleteAsync(int id)
    {
        await _precificacaoRepository.DeleteAsync(id);
    }
}
