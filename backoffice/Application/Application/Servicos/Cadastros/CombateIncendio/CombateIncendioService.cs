using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Cadastros.ContratoPrestacaoServico;
using Domain.Interfaces.Cadastros.IdentificadorIncendio;
using Domain.Interfaces.User;
using Helpers;
using Infra.Repositorio.Cadastros.IdentificadorIncendio;

namespace Application.Application.Servicos.Cadastros.CombateIncendio;

public class CombateIncendioService : ICombateIncendioService
{
    private readonly ICombateIncendioDecolagemPousoRepository _combateIncendioDecolagemPousoRepository;
    private readonly IContratoPrestacaoServicoRepository _contratoPrestacaoServicoRepository;
    private readonly ICombateIncendioRepository _combateIncendioRepository;
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IIdentificadorIncendioRepository _identificadorIncendioRepository;
    private readonly IMapper _mapper;

    public CombateIncendioService(
        ICombateIncendioDecolagemPousoRepository combateIncendioDecolagemPousoRepository,
        IContratoPrestacaoServicoRepository contratoPrestacaoServicoRepository,
        ICombateIncendioRepository combateIncendioRepository,
        IAeronaveRepository aeronaveRepository,
        IUsuarioRepository usuarioRepository,
        IIdentificadorIncendioRepository identificadorIncendioRepository,
        IMapper mapper
        )
    {
        _combateIncendioDecolagemPousoRepository = combateIncendioDecolagemPousoRepository;
        _contratoPrestacaoServicoRepository = contratoPrestacaoServicoRepository;
        _combateIncendioRepository = combateIncendioRepository;
        _aeronaveRepository = aeronaveRepository;
        _usuarioRepository = usuarioRepository;
        _identificadorIncendioRepository = identificadorIncendioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync(DateTime? offsetDate, string? userId)
    {
        var user = await _usuarioRepository.GetByUserIdAsync(userId);
        var list = await _combateIncendioRepository.GetAllAsync(offsetDate, user.Id, user.Nome);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<CombateIncendioViewModel> GetByIdAsync(int id)
    {
        var obj = await _combateIncendioRepository.GetByIdAsync(id);
        return _mapper.Map<CombateIncendioViewModel>(obj);
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task UpdateDataAlteracaoAsync(int? id)
    {
        try
        {
            await _combateIncendioRepository.UpdateDataAlteracaoAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Não foi possível atualizar a DataAlteracao para o ID {id}.", ex);
        }
    }

    public async Task<ExportRelatorioViewModel> ExportExcelAsync(int? id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var ci = await _combateIncendioRepository.ExportExcelAsync(id);
        var cidp = await _combateIncendioDecolagemPousoRepository.GetByCombateIncendioIdAsync(id);
        var qtdExecucao = cidp?.Count() ?? 0;
        var aeronave = await _aeronaveRepository.GetByIdAsync(ci.IdAeronave, idEmpresaInt);
        var tipoAeronave = "";
        string capacidadeString = ci.CapacidadeCargaAeronave;
        string capacidadeSemPonto = capacidadeString.Replace(".", "");
        int capacidadeCargaAeronave = Convert.ToInt32(capacidadeSemPonto);
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
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusMapaAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByStatusMapaMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task<IEnumerable<CombateIncendioViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids, int isMapa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _combateIncendioRepository.GetListByIdsAsync(ids, idEmpresaInt, statusEnvio, isMapa);
        return _mapper.Map<IEnumerable<CombateIncendioViewModel>>(list);
    }

    public async Task CancelarAsync(int id)
    {
        var relatorioExistente = await _combateIncendioRepository.GetByIdAsync(id);
        if (relatorioExistente != null)
        {
            relatorioExistente.StatusEnvio = 4;

            // Obtendo a hora local do Brasil
            var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var dataAlteracao = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brasilTimeZone);

            relatorioExistente.DataAlteracao = dataAlteracao;

            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(relatorioExistente);

            await _combateIncendioRepository.CancelarAsync(mapProduto);
        }

    }


    public async Task<CombateIncendioViewModel> AddAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        var contratoPrestacao = new ContratoPrestacaoServicoViewModel();
        if (obj.ContratoPrestacaoServicoId != null && obj.ContratoPrestacaoServicoId != 0)
        {
            var contratoPrestacaoEntity = await _contratoPrestacaoServicoRepository.GetByIdAsync(obj.ContratoPrestacaoServicoId, idEmpresaInt);
            contratoPrestacao = _mapper.Map<ContratoPrestacaoServicoViewModel>(contratoPrestacaoEntity);
        }

        mapCombateIncendio.NomeRelatorio = $"Combate Incendio - {mapCombateIncendio.Referencia} - {mapCombateIncendio.Cliente} - {mapCombateIncendio.DataCriacao:dd/MM/yyyy} - {contratoPrestacao.Extensao} horas";
        mapCombateIncendio.RefDocument = await _identificadorIncendioRepository.AddAsync(idEmpresaInt);
        await _combateIncendioRepository.AddAsync(mapCombateIncendio);
        return _mapper.Map<CombateIncendioViewModel>(mapCombateIncendio);
    }

    public async Task<CombateIncendioViewModel> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa)
    {

        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var executor = await _usuarioRepository.GetUserByIdAsync(obj.IdExecutor);
        var mapCombateIncendio = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(obj);
        mapCombateIncendio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        var contratoPrestacao = new ContratoPrestacaoServicoViewModel();
        if (obj.ContratoPrestacaoServicoId != null && obj.ContratoPrestacaoServicoId != 0)
        {
            var contratoPrestacaoEntity = await _contratoPrestacaoServicoRepository.GetByIdAsync(obj.ContratoPrestacaoServicoId, idEmpresaInt);
            contratoPrestacao = _mapper.Map<ContratoPrestacaoServicoViewModel>(contratoPrestacaoEntity);
        }

        mapCombateIncendio.NomeRelatorio = $"Combate Incendio - {mapCombateIncendio.Id} - {mapCombateIncendio.Referencia} - {mapCombateIncendio.Cliente} - {mapCombateIncendio.DataCriacao:dd/MM/yyyy} - {contratoPrestacao.Extensao} horas";
        
        await _combateIncendioRepository.UpdateAsync(mapCombateIncendio);
        return _mapper.Map<CombateIncendioViewModel>(mapCombateIncendio);
    }

    public async Task UpdateIsMapaAsync(List<int> relatorios, bool condicao)
    {
        foreach (var relatorio in relatorios)
        {
            var relatorioExistente = await _combateIncendioRepository.GetByIdAsync(relatorio);
            if (relatorioExistente != null)
            {
                relatorioExistente.IsMapa = condicao;

                // Obtendo a hora local do Brasil
                var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var dataAlteracao = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brasilTimeZone);

                relatorioExistente.DataAlteracao = dataAlteracao;

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
