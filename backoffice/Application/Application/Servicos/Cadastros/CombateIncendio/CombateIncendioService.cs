using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Helpers;

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

    public async Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync(DateTime? offsetDate, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _combateIncendioRepository.GetAllAsync(offsetDate, idEmpresaInt);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<CombateIncendioViewModel> GetByIdAsync(int id)
    {
        var obj = await _combateIncendioRepository.GetByIdAsync(id);
        return _mapper.Map<CombateIncendioViewModel>(obj);
    }

    public async Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        var combateIncendio = await _combateIncendioRepository.AddAsync(mapCombateIncendio);
        return combateIncendio;
    }

    public async Task<int> UpdateAsync(CombateIncendioViewModel obj,string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        return await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
    }

    public async Task DeleteAsync(int id)
    {
        await _combateIncendioRepository.DeleteAsync(id);
    }
}
