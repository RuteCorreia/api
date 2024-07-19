using Application.Application.Servicos.Importação_Planilha;
using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.BulaAplicacao.Interface;
using Application.DTOs.Importação_Planilha;
using Application.DTOs.Log.Interface;
using Domain.Entidades.Cadastros.Empresa;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
//[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BulaController : ControllerBase
{
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IBulaAplicacaoService _bulaAplicacaoService;
    private readonly ILogService _loggerService;
    private readonly IBulaService _bulaService;
    public BulaController(
        IBulaAplicacaoService bulaAplicacaoService,
        LoggedUserInfoService loggedUserInfoService,
        ILogService loggerService,
        IBulaService bulaService
        )
    {
        _loggedUserInfoService = loggedUserInfoService;
        _bulaAplicacaoService = bulaAplicacaoService;
        _loggerService = loggerService;
        _bulaService = bulaService;      
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<BulaViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var bulas = await _bulaService.GetAllAsync(loggedUser.Item3);
            _loggerService.LogInformation("Todos os registros de Bulas foram recuperados com sucesso.");
            return Ok(bulas);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Bulas: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Bulas: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BulaViewModel>> GetById(int id)
    {
        try
        {
            var bula = await _bulaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(bula))
            {
                _loggerService.LogInformation($"A Bula com ID {id} foi recuperada com sucesso.");
                return Ok(bula);
            }

            _loggerService.LogWarning($"A Bula com ID {id} não foi encontrada.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar a Bula com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar a Bula: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] BulaViewModel obj)
    {
        try
        {
            //var verificaSeBulaExistePeloNome = _bulaService.GetByName(obj.NomeProduto).Result;
            //if (verificaSeBulaExistePeloNome != null)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma bula com esse nome!");
            //}
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(loggedUser.Item3);
                obj.IdEmpresa = idEmpresaInt;
                await _bulaService.AddAsync(obj);
                var lista = await _bulaService.GetAllAsync(loggedUser.Item3);
                var ultimoCriado = lista.LastOrDefault();
                foreach (var item in obj.BulaAplicacoes)
                {
                    item.IdBula = ultimoCriado.IdBula;
                    await _bulaAplicacaoService.AddAsync(item);

                }
                _loggerService.LogInformation("Bula adicionada com sucesso.");
                return Ok();
            }

            _loggerService.LogWarning("Tentativa de adicionar uma Bula com um modelo inválido.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao adicionar Bula: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar Bula: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] BulaViewModel obj)
    {
        try
        {
            var verificaSeBulaExistePeloNome = _bulaService.GetByName(obj.NomeProduto).Result;
            if (verificaSeBulaExistePeloNome != null && verificaSeBulaExistePeloNome.IdBula != obj.IdBula)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma bula com esse nome!");
            }
            if (ModelState.IsValid)
            {
                var objeto = await _bulaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.IdBula = objeto.IdBula;

                    await _bulaService.UpdateAsync(obj);

                    foreach (var item in obj.BulaAplicacoes)
                    {
                        item.IdBula = id;
                        item.IdBulaAplicacao = 0;
                        await _bulaAplicacaoService.AddAsync(item);

                    }
                    _loggerService.LogInformation($"Bula com ID {id} atualizada com sucesso.");
                    return Ok();
                }
                else
                {
                    _loggerService.LogWarning($"A Bula com ID {id} não foi encontrada.");
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _loggerService.LogWarning("Tentativa de atualizar uma Bula com um modelo inválido.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao atualizar Bula com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Bula: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _bulaService.DeleteAsync(id);
                _loggerService.LogInformation($"Bula com ID {id} deletada com sucesso.");
                return Ok();
            }

            _loggerService.LogWarning("Solicitação para deletar Bula não pôde ser executada, ID inválido.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não pôde ser executada");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao deletar Bula: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Bula: {ex.Message}");
        }
    }

    [HttpPost("ImportarPlanilha")]
    public async Task<ActionResult> ImportarPlanilha([FromBody] string base64)
    {
        try
        {
            var configuracao = new Application.DTOs.Importação_Planilha.ViewModel.ConfiguracoesPlanilhaViewModel()
            {
                IdImportacao = 0,
                EnderecoPlanilha = base64,
                QtdRegistroBanco = 0,
                QtdRegistroPlanilha = 0,
                DadosSalvos = false,
                IdCliente = 0,
                IndexLinhaUltimaCarga = 0,
                DataPlanilha = DateTime.Now.ToString(),

            };
            new ServicosPlanilha<Bula>(configuracao, "Bula").SalvarPlanilha(base64);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ImportarPlanilha - {ex.Message}");
        }
    }

    [HttpPost("ProcessarPlanilha")]
    public async Task<ActionResult> ProcessarPlanilhas()
    {
        try
        {
            var configuracao = new Application.DTOs.Importação_Planilha.ViewModel.ConfiguracoesPlanilhaViewModel()
            {
                IdImportacao = 0,
                EnderecoPlanilha = "",
                QtdRegistroBanco = 0,
                QtdRegistroPlanilha = 0,
                DadosSalvos = false,
                IdCliente = 0,
                IndexLinhaUltimaCarga = 0,
                DataPlanilha = DateTime.Now.ToString(),

            };
            var config = new ServicosPlanilha<Bula>(configuracao, "Bula").BuscarPlanilhaNaFila();

            new ServicosPlanilha<Bula>(config, "Bula").IniciarProcessamento();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ImportarPlanilha - {ex.Message}");
        }
    }
}
