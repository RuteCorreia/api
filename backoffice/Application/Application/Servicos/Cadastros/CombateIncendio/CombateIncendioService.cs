using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.User;
using Helpers;

namespace Application.Application.Servicos.Cadastros.CombateIncendio;

public class CombateIncendioService : ICombateIncendioService
{
    private readonly ICombateIncendioRepository _combateIncendioRepository;
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public CombateIncendioService(
        ICombateIncendioRepository combateIncendioRepository,
        IAeronaveRepository aeronaveRepository,
        IUsuarioRepository usuarioRepository,
        IMapper mapper
        )
    {
        _combateIncendioRepository = combateIncendioRepository;
        _aeronaveRepository = aeronaveRepository;
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

    public async Task<ExportRelatorioViewModel> ExportExcelAsync(int? id)
    {
        var ci = await _combateIncendioRepository.ExportExcelAsync(id);
        var aeronave = await _aeronaveRepository.GetByIdAsync(ci.IdAeronave);
        var tipoAeronave = "";

        TimeSpan duration = TimeSpan.Zero;
        // Convertendo as strings de hora para TimeSpan
        var horaInicio = ci.HoraInicial;
        var HoraTermino = ci.HorarioFinalOperacao;
        // Calculando a diferença de tempo

        if (HoraTermino.HasValue && horaInicio.HasValue)
        {
            duration = HoraTermino.Value - horaInicio.Value;
            // Faça algo com 'duration'
        }


        int hours = Math.Abs(duration.Hours);
        int minutes = Math.Abs(duration.Minutes);

        string horasIncendio = $"{hours}{minutes:D2}";

        if (aeronave.Tipo == Domain.Enums.ETipoAeronave.Drone)
        {
            tipoAeronave = "Drone";
        } else if(aeronave.Tipo == Domain.Enums.ETipoAeronave.Aeronave)
        {
            tipoAeronave = "Convencional";
        }

        var viewModel = new ExportRelatorioViewModel
        {
            UF = ci.Uf,
            Municipio = ci.Cidade,
            TipoAeronave = tipoAeronave,
            PrefixoAeronave = aeronave.Prefixo,
            Volume = ci.TotalAguaUtilizadaOperacao,
            HorasCombateIncendio = horasIncendio
        };
        return viewModel;
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

    public async Task<int> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var executor = await _usuarioRepository.GetUserByIdAsync(obj.IdExecutor);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        return await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
    }

    public async Task UpdateIsMapaAsync(List<int> relatorios)
    {
        foreach (var relatorio in relatorios)
        {
            var relatorioExistente = await _combateIncendioRepository.GetByIdAsync(relatorio);
            if (relatorioExistente != null)
            {
                relatorioExistente.IsMapa = true;

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
