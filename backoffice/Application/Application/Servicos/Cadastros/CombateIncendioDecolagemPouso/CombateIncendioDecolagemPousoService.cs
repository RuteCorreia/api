using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;

namespace Application.Application.Servicos.Cadastros.CombateIncendioDecolagemPouso;

public class CombateIncendioDecolagemPousoService : ICombateIncendioDecolagemPousoService
{
    private readonly ICombateIncendioDecolagemPousoRepository _combateIncendioDecolagemPousoRepository;
    private readonly IMapper _mapper;

    public CombateIncendioDecolagemPousoService(IMapper mapper, ICombateIncendioDecolagemPousoRepository combateIncendioDecolagemPousoRepository)
    {
        _combateIncendioDecolagemPousoRepository = combateIncendioDecolagemPousoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetAllAsync()
    {
        var list = await _combateIncendioDecolagemPousoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CombateIncendioDecolagemPousoViewModel>>(list);
    }

    public async Task<CombateIncendioDecolagemPousoViewModel> GetByIdAsync(int id)
    {
        var obj = await _combateIncendioDecolagemPousoRepository.GetByIdAsync(id);
        return _mapper.Map<CombateIncendioDecolagemPousoViewModel>(obj);
    }

    public async Task<int> AddAsync(CombateIncendioDecolagemPousoViewModel obj)
    {
        var mapCombateIncendioDecolagemPouso = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(obj);
        var id = await _combateIncendioDecolagemPousoRepository.AddAsync(mapCombateIncendioDecolagemPouso);
        return id;
    }

    public async Task UpdateAsync(CombateIncendioDecolagemPousoViewModel obj)
    {
        var mapCombateIncendioDecolagemPouso = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(obj);
        await _combateIncendioDecolagemPousoRepository.UpdateAsync(mapCombateIncendioDecolagemPouso);
    }

    public async Task DeleteAsync(int id)
    {
        await _combateIncendioDecolagemPousoRepository.DeleteAsync(id);
    }
}
