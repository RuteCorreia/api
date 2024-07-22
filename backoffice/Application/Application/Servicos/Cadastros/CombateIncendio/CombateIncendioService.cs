using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.User;
using Helpers;

namespace Application.Application.Servicos.Cadastros.CombateIncendio;

public class CombateIncendioService : ICombateIncendioService
{
    private readonly ICombateIncendioDecolagemPousoRepository _combateIncendioDecolagemPousoRepository;
    private readonly ICombateIncendioRepository _combateIncendioRepository;
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public CombateIncendioService(
        ICombateIncendioDecolagemPousoRepository combateIncendioDecolagemPousoRepository,
        ICombateIncendioRepository combateIncendioRepository,
        IAeronaveRepository aeronaveRepository,
        IUsuarioRepository usuarioRepository,
        IMapper mapper
        )
    {
        _combateIncendioDecolagemPousoRepository = combateIncendioDecolagemPousoRepository;
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
        var cidp = await _combateIncendioDecolagemPousoRepository.GetByCombateIncendioIdAsync(id);
        var qtdExecucao = cidp?.Count() ?? 0;
        var aeronave = await _aeronaveRepository.GetByIdAsync(ci.IdAeronave);
        var tipoAeronave = "";
        int capacidadeCargaAeronave = Convert.ToInt32(ci.CapacidadeCargaAeronave);
        var volume = qtdExecucao * capacidadeCargaAeronave;

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
            Volume = volume,
            HorasCombateIncendio = horasIncendio,
            TipoDeServico = "COMBATE A INCÊNDIO"
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

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusMapaMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        TimeSpan duration = TimeSpan.Zero;
        // Convertendo as strings de hora para TimeSpan
        var horaInicio = mapCombateIncendio.HoraInicial;
        var HoraTermino = mapCombateIncendio.HorarioFinalOperacao;
        // Calculando a diferença de tempo

        if (HoraTermino.HasValue && horaInicio.HasValue)
        {
            duration = HoraTermino.Value - horaInicio.Value;
        }


        int hours = Math.Abs(duration.Hours);
        int minutes = Math.Abs(duration.Minutes);
        int seconds = Math.Abs(duration.Seconds);

        string horasIncendio;
        if (hours > 0 || minutes > 0 || seconds > 0)
        {
            horasIncendio = "";

            if (hours > 0)
            {
                horasIncendio += $"{hours} hora{(hours > 1 ? "s" : "")}";
                if (minutes > 0 || seconds > 0)
                    horasIncendio += " ";
            }

            if (minutes > 0)
            {
                horasIncendio += $"{minutes} minuto{(minutes > 1 ? "s" : "")}";
                if (seconds > 0)
                    horasIncendio += " e ";
            }

            if (seconds > 0)
            {
                horasIncendio += $"{seconds} segundo{(seconds > 1 ? "s" : "")}";
            }
        }
        else
        {
            horasIncendio = "Menos de um minuto";
        }

        mapCombateIncendio.NomeRelatorio = $"Combate Incendio - {mapCombateIncendio.Cliente} - {mapCombateIncendio.DataAlteracao} - {horasIncendio}";
        var combateIncendio = await _combateIncendioRepository.AddAsync(mapCombateIncendio);
        return combateIncendio;
    }

    public async Task<int> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {

        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var executor = await _usuarioRepository.GetUserByIdAsync(obj.IdExecutor);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        TimeSpan duration = TimeSpan.Zero;
        // Convertendo as strings de hora para TimeSpan
        var horaInicio = mapCombateIncendio.HoraInicial;
        var HoraTermino = mapCombateIncendio.HorarioFinalOperacao;
        // Calculando a diferença de tempo

        if (HoraTermino.HasValue && horaInicio.HasValue)
        {
            duration = HoraTermino.Value - horaInicio.Value;
        }


        int hours = Math.Abs(duration.Hours);
        int minutes = Math.Abs(duration.Minutes);
        int seconds = Math.Abs(duration.Seconds);

        string horasIncendio;
        if (hours > 0 || minutes > 0 || seconds > 0)
        {
            horasIncendio = "";

            if (hours > 0)
            {
                horasIncendio += $"{hours} hora{(hours > 1 ? "s" : "")}";
                if (minutes > 0 || seconds > 0)
                    horasIncendio += " ";
            }

            if (minutes > 0)
            {
                horasIncendio += $"{minutes} minuto{(minutes > 1 ? "s" : "")}";
                if (seconds > 0)
                    horasIncendio += " e ";
            }

            if (seconds > 0)
            {
                horasIncendio += $"{seconds} segundo{(seconds > 1 ? "s" : "")}";
            }
        }
        else
        {
            horasIncendio = "Menos de um minuto";
        }

        mapCombateIncendio.NomeRelatorio = $"Combate Incendio - {mapCombateIncendio.Id} - {mapCombateIncendio.Cliente} - {mapCombateIncendio.DataAlteracao} - {horasIncendio}";
        return await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
    }

    public async Task UpdateIsMapaAsync(List<int> relatorios, bool condicao)
    {
        foreach (var relatorio in relatorios)
        {
            var relatorioExistente = await _combateIncendioRepository.GetByIdAsync(relatorio);
            if (relatorioExistente != null)
            {
                relatorioExistente.IsMapa = condicao;

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
