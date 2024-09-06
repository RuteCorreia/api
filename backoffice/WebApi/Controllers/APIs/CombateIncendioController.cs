using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;
using Application.DTOs.Cadastros.RelatorioBase;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.Application.Servicos.Cadastros.RelatorioAplicacao;
using Application.Application.Servicos.Log;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using System.IO.Compression;
namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CombateIncendioController : ControllerBase
{
    private readonly ICombateIncendioService _combateIncendioService;
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IDataRelatorioService _dataRelatorioService;
    private readonly ILogService _loggerService;

    public CombateIncendioController(
        ICombateIncendioService combateIncendioService,
        LoggedUserInfoService loggedUserInfoService,
        IDataRelatorioService dataRelatorioService,
        ILogService loggerService)
    {
        _combateIncendioService = combateIncendioService;
        _loggedUserInfoService = loggedUserInfoService;
        _dataRelatorioService = dataRelatorioService;
        _loggerService = loggerService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CombateIncendioViewModel>>> GetAll(DateTime? date)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var combateIncendio = await _combateIncendioService.GetAllAsync(date, loggedUser.Item1);
            _loggerService.LogInformation("Todos os registros de Combate a Incêndio foram recuperados com sucesso.");
            return Ok(combateIncendio);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CombateIncendioViewModel>> GetById(int id)
    {
        try
        {
            var combateIncendio = await _combateIncendioService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(combateIncendio))
            {
                _loggerService.LogInformation($"Registro de Combate a Incêndio com ID {id} foi recuperado com sucesso.");
                return Ok(combateIncendio);
            }

            _loggerService.LogWarning($"Registro de Combate a Incêndio com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar registro de Combate a Incêndio com ID {id}: {ex.Message}");
        }
    }

    [HttpGet("getDataFromApp")]
    public async Task<ActionResult<IEnumerable<CombateIncendioViewModel>>> GetDataFromApp()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var relatorios = await _combateIncendioService.GetListByStatusAsync(loggedUser.Item3);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                if (!string.IsNullOrEmpty(data.Data))
                {
                    var relatorioBaseViewModel = new RelatorioBaseViewModel
                    {
                        NomeRelatorio = relatorio.NomeRelatorio,
                        Base64Data = data.Data,
                        IsMapa = relatorio.IsMapa,
                        Id = relatorio.Id
                    };

                    dataRelatorios.Add(relatorioBaseViewModel);
                }
                else
                {
                    // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                    _loggerService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                }
            }

            _loggerService.LogInformation("Todos os relatórios de combate a incêndio foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
        }
    }

    [HttpGet("getRelatorioMapa")]
    public async Task<ActionResult<IEnumerable<CombateIncendioViewModel>>> GetRelatorioMapa()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var relatorios = await _combateIncendioService.GetListByStatusMapaAsync(loggedUser.Item3);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                if (!string.IsNullOrEmpty(data.Data))
                {
                    var relatorioBaseViewModel = new RelatorioBaseViewModel
                    {
                        NomeRelatorio = relatorio.NomeRelatorio,
                        Base64Data = data.Data,
                        IsMapa = relatorio.IsMapa,
                        Id = relatorio.Id
                    };

                    dataRelatorios.Add(relatorioBaseViewModel);
                }
                else
                {
                    // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                    _loggerService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                }
            }

            _loggerService.LogInformation("Todos os relatórios de combate a incêndio foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
        }
    }

    [HttpGet("getRelatoriosMapaMes/{mes}/{ano}")]
    public async Task<ActionResult<IEnumerable<CombateIncendioViewModel>>> GetRelatoriosMapaMes(int mes, int ano)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

            // Primeiro dia do mês e último dia do mês
            var primeiroDiaMes = new DateTime(ano, mes, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            var relatorios = await _combateIncendioService.GetListByStatusMapaMesAsync(loggedUser.Item3, primeiroDiaMes, ultimoDiaMes);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                if (!string.IsNullOrEmpty(data.Data))
                {
                    var relatorioBaseViewModel = new RelatorioBaseViewModel
                    {
                        NomeRelatorio = relatorio.NomeRelatorio,
                        Base64Data = data.Data,
                        IsMapa = relatorio.IsMapa,
                        Id = relatorio.Id
                    };

                    dataRelatorios.Add(relatorioBaseViewModel);
                }
                else
                {
                    // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                    _loggerService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                }
            }

            _loggerService.LogInformation("Todos os relatórios de combate a incêndio foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
        }
    }

    [HttpGet("GetRelatoriosMes/{mes}/{ano}")]
    public async Task<ActionResult<IEnumerable<CombateIncendioViewModel>>> GetRelatoriosMes(int mes, int ano)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

            // Primeiro dia do mês e último dia do mês
            var primeiroDiaMes = new DateTime(ano, mes, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            var relatorios = await _combateIncendioService.GetListByMesAsync(loggedUser.Item3, primeiroDiaMes, ultimoDiaMes);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                if (!string.IsNullOrEmpty(data.Data))
                {
                    var relatorioBaseViewModel = new RelatorioBaseViewModel
                    {
                        NomeRelatorio = relatorio.NomeRelatorio,
                        Base64Data = data.Data,
                        IsMapa = relatorio.IsMapa,
                        Id = relatorio.Id
                    };

                    dataRelatorios.Add(relatorioBaseViewModel);
                }
                else
                {
                    // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                    _loggerService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                }
            }

            _loggerService.LogInformation("Todos os relatórios de combate a incêndio foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de combate a incêndio: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CombateIncendioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var id = await _combateIncendioService.AddAsync(obj, loggedUser.Item3);
                _loggerService.LogInformation("Novo registro de Combate a Incêndio adicionado com sucesso.");
                return Ok(id);
            }

            _loggerService.LogWarning("Modelo inválido ao adicionar novo registro de Combate a Incêndio.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao adicionar novo registro de Combate a Incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo registro de Combate a Incêndio: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] CombateIncendioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (!ObjectNullValidation.IsObjectNull(obj))
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _combateIncendioService.UpdateAsync(obj, loggedUser.Item3);
                    return Ok(id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _loggerService.LogWarning("Modelo inválido ao atualizar registro de Combate a Incêndio.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio update - {ex.Message}");
        }
    }

    [HttpPut("updateIsmapa/{condicao}")]
    public async Task<ActionResult> UpdateIsMapa([FromBody] List<int> obj, bool condicao)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _combateIncendioService.UpdateIsMapaAsync(obj, condicao);
                _loggerService.LogInformation("Campo IsMapa do relatório de aplicação atualizado com sucesso.");
                return Ok();
            }

            _loggerService.LogWarning("Modelo inválido ao atualizar campo IsMapa do relatório de aplicação.");
            return BadRequest("Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao atualizar campo IsMapa do relatório de aplicação: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar campo IsMapa do relatório de aplicação: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _combateIncendioService.DeleteAsync(id);
                _loggerService.LogInformation($"Registro de Combate a Incêndio com ID {id} deletado com sucesso.");
                return Ok("Deletado com sucesso");
            }
            _loggerService.LogWarning($"Registro de Combate a Incêndio com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao deletar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio delete - {ex.Message}");
        }
    }

    [HttpGet("DownloadRelatoriosMes")]
    public async Task<IActionResult> DownloadRelatoriosMes([FromQuery] List<int> ids, [FromQuery] int mes, [FromQuery] int ano)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

            var relatorios = await _combateIncendioService.GetListByIdsAsync(loggedUser.Item3, ids);

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var relatorio in relatorios)
                    {
                        var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                        if (!string.IsNullOrEmpty(data.Data))
                        {
                            var relatorioBytes = Convert.FromBase64String(data.Data);
                            var nomeArquivo = $"{relatorio.NomeRelatorio}.pdf".Replace("/", "-").Replace("\\", "-");
                            var entry = archive.CreateEntry(nomeArquivo, CompressionLevel.Fastest);

                            using (var entryStream = entry.Open())
                            {
                                entryStream.Write(relatorioBytes, 0, relatorioBytes.Length);
                            }
                        }
                        else
                        {
                            _loggerService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                        }
                    }
                }

                // Ajuste o ponteiro do stream para o início
                memoryStream.Seek(0, SeekOrigin.Begin);

                return File(memoryStream.ToArray(), "application/zip", $"relatorios-{mes}-{ano}.zip");
            }
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao recuperar e compactar os relatórios de combate a incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar e compactar os relatórios de combate a incêndio: {ex.Message}");
        }
    }
}
