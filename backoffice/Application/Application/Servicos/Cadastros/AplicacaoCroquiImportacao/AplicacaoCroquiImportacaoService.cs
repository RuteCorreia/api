using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Interface;
using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;

namespace Application.Application.Servicos.Cadastros.AplicacaoCroquiImportacao;

public class AplicacaoCroquiImportacaoService : IAplicacaoCroquiImportacaoService
{
    private readonly IAplicacaoCroquiImportacaoRepository _aplicacaoCroquiImportacaoRepository;
    private readonly IMapper _mapper;

    public AplicacaoCroquiImportacaoService(IMapper mapper, IAplicacaoCroquiImportacaoRepository aplicacaoCroquiImportacaoRepository)
    {
        _aplicacaoCroquiImportacaoRepository = aplicacaoCroquiImportacaoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoCroquiImportacaoViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoCroquiImportacaoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoCroquiImportacaoViewModel>>(list);
    }

    public async Task<AplicacaoCroquiImportacaoViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoCroquiImportacaoRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoCroquiImportacaoViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoCroquiImportacaoViewModel obj)
    {
        var mapAplicacaoCroquiImportacao = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>(obj);
        await _aplicacaoCroquiImportacaoRepository.AddAsync(mapAplicacaoCroquiImportacao);
    }

    public async Task UpdateAsync(AplicacaoCroquiImportacaoViewModel obj)
    {
        var mapAplicacaoCroquiImportacao = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>(obj);
        await _aplicacaoCroquiImportacaoRepository.UpdateAsync(mapAplicacaoCroquiImportacao);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoCroquiImportacaoRepository.DeleteAsync(id);
    }
}
