using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Helpers;

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

    public async Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _combateIncendioDecolagemPousoRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<CombateIncendioDecolagemPousoViewModel>>(list);
    }

    public async Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetByCombateIncendioIdAsync(int? combateIncendioId)
    {
        var list = await _combateIncendioDecolagemPousoRepository.GetByCombateIncendioIdAsync(combateIncendioId);
        return _mapper.Map<IEnumerable<CombateIncendioDecolagemPousoViewModel>>(list);
    }

    public async Task<CombateIncendioDecolagemPousoViewModel> GetByIdAsync(int id)
    {
        var obj = await _combateIncendioDecolagemPousoRepository.GetByIdAsync(id);
        return _mapper.Map<CombateIncendioDecolagemPousoViewModel>(obj);
    }

    public async Task<int> AddAsync(CombateIncendioDecolagemPousoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCombateIncendioDecolagemPouso = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(obj);
        mapCombateIncendioDecolagemPouso.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        var id = await _combateIncendioDecolagemPousoRepository.AddAsync(mapCombateIncendioDecolagemPouso);
        return id;
    }

    public async Task<int> UpdateAsync(CombateIncendioDecolagemPousoViewModel obj)
    {
        var mapCombateIncendioDecolagemPouso = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(obj);
        var result = await _combateIncendioDecolagemPousoRepository.UpdateAsync(mapCombateIncendioDecolagemPouso);
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        await _combateIncendioDecolagemPousoRepository.DeleteAsync(id);
    }
}
