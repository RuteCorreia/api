using Application.DTOs.Cadastros.PlanoDeContrato.Interface;
using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.PlanoDeContrato;

namespace Application.Application.Servicos.Cadastros.PlanoDeContrato;

public class PlanoDeContratoService : IPlanoDeContratoService
{
    private readonly IPlanoDeContratoRepository _planoDeContratoRepository;
    private readonly IMapper _mapper;

    public PlanoDeContratoService(IMapper mapper, IPlanoDeContratoRepository planoDeContratoRepository)
    {
        _planoDeContratoRepository = planoDeContratoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlanoDeContratoViewModel>> GetAllAsync()
    {
        var list = await _planoDeContratoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PlanoDeContratoViewModel>>(list);
    }

    public async Task<PlanoDeContratoViewModel> GetByIdAsync(int id)
    {
        var obj = await _planoDeContratoRepository.GetByIdAsync(id);
        return _mapper.Map<PlanoDeContratoViewModel>(obj);
    }

    public async Task AddAsync(PlanoDeContratoViewModel obj)
    {
        var mapPlanoDeContrato = _mapper.Map<Domain.Entidades.Cadastros.Empresa.PlanoDeContrato>(obj);
        await _planoDeContratoRepository.AddAsync(mapPlanoDeContrato);
    }

    public async Task UpdateAsync(PlanoDeContratoViewModel obj)
    {
        var mapPlanoDeContrato = _mapper.Map<Domain.Entidades.Cadastros.Empresa.PlanoDeContrato>(obj);
        await _planoDeContratoRepository.UpdateAsync(mapPlanoDeContrato);
    }

    public async Task DeleteAsync(int id)
    {
        await _planoDeContratoRepository.DeleteAsync(id);
    }
}
