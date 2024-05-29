using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorio;

public class AplicacaoRelatorioService : IAplicacaoRelatorioService
{
    private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;
    private readonly IMapper _mapper;

    public AplicacaoRelatorioService(IMapper mapper, IAplicacaoRelatorioRepository aplicacaoRelatorioRepository)
    {
        _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoRelatorioViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRelatorioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoRelatorioViewModel>>(list);
    }

    public async Task<AplicacaoRelatorioViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRelatorioRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoRelatorioViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoRelatorioViewModel obj)
    {
        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        await _aplicacaoRelatorioRepository.AddAsync(mapAplicacaoRelatorio);
    }

    public async Task UpdateAsync(AplicacaoRelatorioViewModel obj)
    {
        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        await _aplicacaoRelatorioRepository.UpdateAsync(mapAplicacaoRelatorio);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRelatorioRepository.DeleteAsync(id);
    }
}
