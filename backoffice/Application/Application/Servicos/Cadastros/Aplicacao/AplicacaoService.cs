using Application.DTOs.Cadastros.Aplicacao.Interface;
using Application.DTOs.Cadastros.Aplicacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Aplicacao;

namespace Application.Application.Servicos.Cadastros.Aplicacao;

public class AplicacaoService : IAplicacaoService
{
    private readonly IAplicacaoRepository _aplicacaoRepository;
    private readonly IMapper _mapper;

    public AplicacaoService(IMapper mapper, IAplicacaoRepository aplicacaoRepository)
    {
        _aplicacaoRepository = aplicacaoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoViewModel>>(list);
    }

    public async Task<AplicacaoViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoViewModel obj)
    {
        var mapAplicacao = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>(obj);
        await _aplicacaoRepository.AddAsync(mapAplicacao);
    }

    public async Task UpdateAsync(AplicacaoViewModel obj)
    {
        var mapAplicacao = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>(obj);
        await _aplicacaoRepository.UpdateAsync(mapAplicacao);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRepository.DeleteAsync(id);
    }
}
