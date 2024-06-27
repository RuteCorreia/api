using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApi.HttpRequestInfo;
using Application.Application.Servicos.Log; // Importe o serviço de log
using Application.DTOs.Log.Interface; // Importe a interface do serviço de log
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class DadosResponsavelController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDadosResponsavelService _dadosResponsavelService;
        private readonly ILogService _logService; // Injete o serviço de log

        public DadosResponsavelController(
            LoggedUserInfoService loggedUserInfoService,
            IDadosResponsavelService dadosResponsavelService,
            ILogService logService // Adicione o serviço de log como parâmetro do construtor
        )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _dadosResponsavelService = dadosResponsavelService;
            _logService = logService; // Atribua o serviço de log
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<DadosResponsavelViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var cadastros = await _dadosResponsavelService.GetAllAsync(loggedUser.Item3);
                return Ok(cadastros);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"DadosResponsavel getAll - {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"DadosResponsavel getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DadosResponsavelViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var obj = await _dadosResponsavelService.GetByIdAsync(id, loggedUser.Item3);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Clear().Append($"DadosResponsavel getById - {ex.Message}").ToString()); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"DadosResponsavel getById - {ex.Message}").ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DadosResponsavelViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _dadosResponsavelService.AddAsync(obj, loggedUser.Item3);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Clear().Append($"DadosResponsavel add - {ex.Message}").ToString()); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"DadosResponsavel add - {ex.Message}").ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] DadosResponsavelViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var objeto = await _dadosResponsavelService.GetByIdAsync(id, loggedUser.Item3);
                    if (objeto is not null)
                    {
                        obj.Id = objeto.Id;
                        await _dadosResponsavelService.UpdateAsync(obj);
                        returnMsg.Clear();
                    }
                    else
                    {
                        returnMsg.Clear().Append("Não encontrado");
                    }
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Clear().Append($"DadosResponsavel update - {ex.Message}").ToString()); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"DadosResponsavel update - {ex.Message}").ToString());
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnMsg = new StringBuilder().Append("Solicitação não foi possível de ser executada");
            try
            {
                if (id != 0)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _dadosResponsavelService.DeleteAsync(id, loggedUser.Item3);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Clear().Append($"DadosResponsavel delete - {ex.Message}").ToString()); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"DadosResponsavel delete - {ex.Message}").ToString());
            }
        }
    }
}
