using Application.Application.Servicos.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.RelatorioBase;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ControleDeFrotaController : ControllerBase
{
    private readonly IControleDeFrotaService _controleDeFrotaService;
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IDataRelatorioService _dataRelatorioService;
    private readonly ILogService _logService; // Injete o serviço de log

    public ControleDeFrotaController(
        IControleDeFrotaService controleDeFrotaService,
        LoggedUserInfoService loggedUserInfoService,
        IDataRelatorioService dataRelatorioService,
        ILogService logService) // Adicione o serviço de log como parâmetro do construtor
    {
        _controleDeFrotaService = controleDeFrotaService;
        _loggedUserInfoService = loggedUserInfoService;
        _dataRelatorioService = dataRelatorioService;
        _logService = logService; // Atribua o serviço de log
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ControleDeFrotaViewModel>>> GetAll(DateTime? date)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var relatorios = await _controleDeFrotaService.GetAllAsync(date, loggedUser.Item1, loggedUser.Item2, loggedUser.Item3);
            return Ok(relatorios);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"ControleDeFrota getAll - {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getAll - {ex.Message}");
        }
    }

    [HttpGet("getDataFromApp")]
    public async Task<ActionResult<IEnumerable<ControleDeFrotaViewModel>>> GetDataFromApp()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var relatorios = await _controleDeFrotaService.GetListByStatusAsync(loggedUser.Item3);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var relatorioBaseViewModel = new RelatorioBaseViewModel
                {
                    NomeRelatorio = relatorio.NomeRelatorio,
                    Id = relatorio.Id,
                    StatusEnvio = relatorio.State
                };

                dataRelatorios.Add(relatorioBaseViewModel);
            }

            _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ControleDeFrotaViewModel>> GetById(int id)
    {
        try
        {
            var controleDeFrota = await _controleDeFrotaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(controleDeFrota))
            {
                return Ok(controleDeFrota);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"ControleDeFrota getById - {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getById - {ex.Message}");
        }
    }

    [HttpGet("GetRelatoriosMes/{mes}/{ano}")]
    public async Task<ActionResult<IEnumerable<CombateIncendioViewModel>>> GetRelatoriosMes(int mes, int ano)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

            var primeiroDiaMes = new DateTime(ano, mes, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            var relatorios = await _controleDeFrotaService.GetListByMesAsync(loggedUser.Item3, primeiroDiaMes, ultimoDiaMes);
            List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
            foreach (var relatorio in relatorios)
            {
                var relatorioBaseViewModel = new RelatorioBaseViewModel
                {
                    NomeRelatorio = relatorio.NomeRelatorio,
                    Id = relatorio.Id,
                    StatusEnvio = relatorio.State
                };

                dataRelatorios.Add(relatorioBaseViewModel);
            }

            _logService.LogInformation("Todos os relatórios de frota foram recuperados com sucesso.");
            return Ok(dataRelatorios);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de frota: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de frota: {ex.Message}");
        }
    }

    [HttpGet("DownloadRelatoriosMes")]
    public async Task<IActionResult> DownloadRelatoriosMes([FromQuery] List<int> ids, [FromQuery] int mes, [FromQuery] int ano)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var isMapa = 0;
            var relatorios = await _controleDeFrotaService.GetListByIdsAsync(loggedUser.Item3, ids, isMapa);

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
                            _logService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                        }
                    }
                }

                // Ajuste o ponteiro do stream para o início
                memoryStream.Seek(0, SeekOrigin.Begin);

                return File(memoryStream.ToArray(), "application/zip", $"relatorios-frota-{mes}-{ano}.zip");
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar e compactar os relatórios de frota: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar e compactar os relatórios de frota: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var id = await _controleDeFrotaService.AddAsync(obj, loggedUser.Item3);
                _logService.LogInformation("ControleDeFrota adicionado com sucesso"); // Registre uma informação de log
                return Ok(id);
            }

            _logService.LogWarning("Tentativa de adição de ControleDeFrota com modelo inválido"); // Registre um aviso de log
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar ControleDeFrota: {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota add - {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var id = await _controleDeFrotaService.UpdateAsync(obj);
                if (id.HasValue)
                {
                    return Ok(id); // Retorna o ID do objeto atualizado
                }
                return NotFound("Objeto não encontrado"); // Caso o objeto não seja encontrado
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar ControleDeFrota: {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _controleDeFrotaService.DeleteAsync(id);
                _logService.LogInformation("ControleDeFrota deletado com sucesso"); // Registre uma informação de log
                return Ok("Deletado com sucesso");
            }
            _logService.LogWarning($"Tentativa de deletar de ControleDeFrota com Id inválido: {id} "); // Registre um aviso de log

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar ControleDeFrota: {ex.Message}"); // Registre um erro de log

            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota delete - {ex.Message}");
        }
    }

    [HttpGet("DownloadArquivo/{id}")]
    public async Task<IActionResult> DownloadArquivo(int id)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var relatorio = await _controleDeFrotaService.GetByIdAsync(id);

            // Obter os dados do relatório
            var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);
            if (!string.IsNullOrEmpty(data.Data))
            {
                // Converter os dados do relatório para bytes
                var relatorioBytes = Convert.FromBase64String(data.Data);

                // Nome do arquivo
                var nomeArquivoRelatorio = $"{relatorio.NomeRelatorio.Replace("/", "-").Replace("\\", "-")}.pdf";

                // Retornar o arquivo PDF
                return File(relatorioBytes, "application/pdf", nomeArquivoRelatorio);
            }

            // Caso não haja dados
            return NotFound("Relatório não encontrado ou não possui dados.");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar o relatório: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar o relatório: {ex.Message}");
        }
    }
}
