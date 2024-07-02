using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.CombateIncendioPista.Interface;
using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CombateIncendioPistaController : ControllerBase
    {
        private readonly ICombateIncendioPistaService _combateIncendioPistaService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        public CombateIncendioPistaController(
            ICombateIncendioPistaService combateIncendioPistaService,
            LoggedUserInfoService loggedUserInfoService)
        {
            _combateIncendioPistaService = combateIncendioPistaService;
            _loggedUserInfoService = loggedUserInfoService;
        }


        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<CombateIncendioPistaViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var combateIncendio = await _combateIncendioPistaService.GetAllAsync(loggedUser.Item3);
                return Ok(combateIncendio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CombateIncendioPistaViewModel>> GetById(int id)
        {
            try
            {
                var combateIncendioPista = await _combateIncendioPistaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(combateIncendioPista))
                {
                    return Ok(combateIncendioPista);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            }
        }

        [HttpGet("GetByCombateIncendioId/{combateIncendioId}")]
        public async Task<ActionResult<IAsyncEnumerable<CombateIncendioPistaViewModel>>> GetByCombateIncendioId(int combateIncendioId)
        {
            try
            {
                var combateIncendio = await _combateIncendioPistaService.GetByCombateIncendioIdAsync(combateIncendioId);
                return Ok(combateIncendio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CombateIncendioPistaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _combateIncendioPistaService.AddAsync(obj, loggedUser.Item3);
                    return Ok(id);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo registro de Combate a Incêndio: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CombateIncendioPistaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ObjectNullValidation.IsObjectNull(obj))
                    {
                        var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                        var id = await _combateIncendioPistaService.UpdateAsync(obj, loggedUser.Item3);
                        return Ok(id);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _combateIncendioPistaService.DeleteAsync(id);
                    return Ok("Deletado com sucesso");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio delete - {ex.Message}");
            }
        }
    }
}
