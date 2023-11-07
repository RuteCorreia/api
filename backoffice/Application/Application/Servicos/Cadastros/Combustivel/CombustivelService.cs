using Application.DTOs.Cadastros.Combustivel.Interface;
using Application.DTOs.Cadastros.Combustivel.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Combustivel;

namespace Application.Application.Servicos.Cadastros.Combustivel;

public class CombustivelService : ICombustivelService
{
    private readonly ICombustivelRepository _combustivelRepository;
    private readonly IMapper _mapper;

    public CombustivelService(IMapper mapper, ICombustivelRepository combustivelRepository) 
    {
        _combustivelRepository = combustivelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CombustivelViewModel>> GetAllAsync()
    {
        var list = await _combustivelRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CombustivelViewModel>>(list);
    }

    public async Task<CombustivelViewModel> GetByIdAsync(int id)
    {
        var obj = await _combustivelRepository.GetByIdAsync(id);
        return _mapper.Map<CombustivelViewModel>(obj);
    }

    public async Task AddAsync(CombustivelViewModel obj)
    {
        var mapCombustivel = _mapper.Map<Domain.Entidades.Cadastros.Combustivel.Combustivel>(obj);
        await _combustivelRepository.AddAsync(mapCombustivel);
    }

    public async Task UpdateAsync(CombustivelViewModel obj)
    {
        var mapCombustivel = _mapper.Map<Domain.Entidades.Cadastros.Combustivel.Combustivel>(obj);
        await _combustivelRepository.UpdateAsync(mapCombustivel);
    }

    public async Task DeleteAsync(int id)
    { 
        await _combustivelRepository.DeleteAsync(id);
    }
}
