using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.User;
using Helpers;

namespace Application.Application.Servicos.Cadastros.CombateIncendio;

public class CombateIncendioService : ICombateIncendioService
{
    private readonly ICombateIncendioRepository _combateIncendioRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public CombateIncendioService(IMapper mapper,IUsuarioRepository usuarioRepository ,ICombateIncendioRepository combateIncendioRepository)
    {
        _combateIncendioRepository = combateIncendioRepository;
        _usuarioRepository = usuarioRepository;
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

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusMapaAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        mapCombateIncendio.NomeRelatorio = $"Combate Incendio - {mapCombateIncendio.Cliente} - {mapCombateIncendio.DataAlteracao}";
        var combateIncendio = await _combateIncendioRepository.AddAsync(mapCombateIncendio);
        return combateIncendio;
    }

    public async Task<int> UpdateAsync(CombateIncendioViewModel obj,string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var executor = await _usuarioRepository.GetUserByIdAsync(obj.IdExecutor);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        return await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
    }

    public async Task UpdateIsMapaAsync(List<CombateIncendioViewModel> relatorios)
    {
        foreach (var relatorio in relatorios)
        {
            var relatorioExistente = await _combateIncendioRepository.GetByIdAsync(relatorio.Id);
            if (relatorioExistente != null)
            {
                relatorioExistente.IsMapa = relatorio.IsMapa;

                var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(relatorioExistente);

                await _combateIncendioRepository.UpdateIsMapaAsync(mapProduto);
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _combateIncendioRepository.DeleteAsync(id);
    }
}
