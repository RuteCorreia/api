using Application.DTOs.Cadastros.Bateria.Interface;
using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.Interface;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class GeradorController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IGeradorService _geradorService;
        public GeradorController(
            LoggedUserInfoService loggedUserInfoService,
            IGeradorService geradorService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _geradorService = geradorService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<GeradorViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var geradores = await _geradorService.GetAllAsync(loggedUser.Item3);
                return Ok(geradores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeradorViewModel>> GetById(int id)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var gerador = await _geradorService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(gerador))
                {
                    return Ok(gerador);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<GeradorViewModel>>> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("O nome não pode ser nulo ou vazio.");
                }

                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var geradores = await _geradorService.GetByNameAsync(name, loggedUser.Item3);

                if (geradores.Any())
                {
                    return Ok(geradores);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Nenhuma pista encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pistas pelo nome: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] GeradorViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _geradorService.AddAsync(obj, loggedUser.Item3);
                    return Ok(id);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico add - {ex.Message}");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] GeradorViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var objeto = await _geradorService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _geradorService.UpdateAsync(obj);
                        return Ok();
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var objeto = await _geradorService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _geradorService.DeleteAsync(id, loggedUser.Item3);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico delete - {ex.Message}");
            }
        }
    }
}
