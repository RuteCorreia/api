using Application.DTOs.Cadastros.Motobomba.Interface;
using Application.DTOs.Cadastros.Motobomba.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class MotobombaController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IMotobombaService _motobombaService;
        public MotobombaController(
            LoggedUserInfoService loggedUserInfoService,
            IMotobombaService motobombaService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _motobombaService = motobombaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<MotobombaViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var motobombas = await _motobombaService.GetAllAsync(loggedUser.Item3);
                return Ok(motobombas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MotobombaViewModel>> GetById(int id)
        {
            try
            {
                var motobomba = await _motobombaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(motobomba))
                {
                    return Ok(motobomba);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<MotobombaViewModel>>> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("O nome não pode ser nulo ou vazio.");
                }

                var motobombas = await _motobombaService.GetByNameAsync(name);

                if (motobombas.Any())
                {
                    return Ok(motobombas);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Nenhuma pista encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pistas pelo nome: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] MotobombaViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _motobombaService.AddAsync(obj, loggedUser.Item3);
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
        public async Task<ActionResult> Update(int id, [FromBody] MotobombaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _motobombaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _motobombaService.UpdateAsync(obj);
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
                if (id != 0)
                {
                    await _motobombaService.DeleteAsync(id);
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
