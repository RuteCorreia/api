using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CombateIncendio;

namespace Application.Application.Servicos.Cadastros.CombateIncendio;

public class CombateIncendioService : ICombateIncendioService
{
    private readonly ICombateIncendioRepository _combateIncendioRepository;
    private readonly IMapper _mapper;

    public CombateIncendioService(IMapper mapper, ICombateIncendioRepository combateIncendioRepository)
    {
        _combateIncendioRepository = combateIncendioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync()
    {
        var list = await _combateIncendioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<CombateIncendioViewModel> GetByIdAsync(int id)
    {
        var obj = await _combateIncendioRepository.GetByIdAsync(id);
        return _mapper.Map<CombateIncendioViewModel>(obj);
    }

    public async Task AddAsync(CombateIncendioViewModel obj)
    {
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        await _combateIncendioRepository.AddAsync(mapCombateIncendio);
    }

    public async Task UpdateAsync(CombateIncendioViewModel obj)
    {
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
    }

    public async Task DeleteAsync(int id)
    {
        await _combateIncendioRepository.DeleteAsync(id);
    }
}
