using Application.Application.Servicos.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Cadastros.RelatorioBase;
using AutoMapper;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.User;
using Helpers;

namespace Application.Application.Servicos.Cadastros.ControleDeFrota;

public class ControleDeFrotaService : IControleDeFrotaService
{
    private readonly IControleDeFrotaRepository _controleDeFrotaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public ControleDeFrotaService( 
        IControleDeFrotaRepository controleDeFrotaRepository,
        IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _controleDeFrotaRepository = controleDeFrotaRepository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetAllAsync(DateTime? offsetDate, string? userId)
    {
        var user = await _usuarioRepository.GetByUserIdAsync(userId);
        var list = await _controleDeFrotaRepository.GetAllAsync(offsetDate, user.Nome);
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetListByStatusAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _controleDeFrotaRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<ControleDeFrotaViewModel> GetByIdAsync(int? id)
    {
        var obj = await _controleDeFrotaRepository.GetByIdAsync(id);
        return _mapper.Map<ControleDeFrotaViewModel>(obj);
    }

    public async Task<int> AddAsync(ControleDeFrotaViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        mapControleDeFrota.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        mapControleDeFrota.NomeRelatorio = $"Frota - {mapControleDeFrota.NomeExecutor} - {mapControleDeFrota.NomePiloto} - {mapControleDeFrota.DataCriacao}";
        var id = await _controleDeFrotaRepository.AddAsync(mapControleDeFrota);
        return id;
    }

    public async Task<int?> UpdateAsync(ControleDeFrotaViewModel obj)
    {
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        mapControleDeFrota.NomeRelatorio = $"Frota - {mapControleDeFrota.NomeExecutor} - {mapControleDeFrota.NomePiloto} - {mapControleDeFrota.DataCriacao}";
        return await _controleDeFrotaRepository.UpdateAsync(mapControleDeFrota);
    }

    public async Task DeleteAsync(int id)
    {
        await _controleDeFrotaRepository.DeleteAsync(id);
    }
}
