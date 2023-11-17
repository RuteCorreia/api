using Application.DTOs.Cadastros.AplicacaoContrato.Interface;
using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoContrato;

namespace Application.Application.Servicos.Cadastros.AplicacaoContrato;

public class AplicacaoContratoService : IAplicacaoContratoService
{
    private readonly IAplicacaoContratoRepository _aplicacaoContratoRepository;
    private readonly IMapper _mapper;

    public AplicacaoContratoService(IMapper mapper, IAplicacaoContratoRepository aplicacaoContratoRepository)
    {
        _aplicacaoContratoRepository = aplicacaoContratoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoContratoViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoContratoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoContratoViewModel>>(list);
    }

    public async Task<AplicacaoContratoViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoContratoRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoContratoViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoContratoViewModel obj)
    {
        var mapAplicacaoContrato = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>(obj);
        await _aplicacaoContratoRepository.AddAsync(mapAplicacaoContrato);
    }

    public async Task UpdateAsync(AplicacaoContratoViewModel obj)
    {
        var mapAplicacaoContrato = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>(obj);
        await _aplicacaoContratoRepository.UpdateAsync(mapAplicacaoContrato);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoContratoRepository.DeleteAsync(id);
    }
}
