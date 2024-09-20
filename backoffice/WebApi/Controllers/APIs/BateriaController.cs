using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Bateria.Interface;
using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
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
    public class BateriaController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IBateriaService _bateriaService;
        public BateriaController(
            LoggedUserInfoService loggedUserInfoService,
            IBateriaService bateriaService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _bateriaService = bateriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<BateriaViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var baterias = await _bateriaService.GetAllAsync(loggedUser.Item3);
                return Ok(baterias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BateriaViewModel>> GetById(int id)
        {
            try
            {
                var bateria = await _bateriaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(bateria))
                {
                    return Ok(bateria);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<BateriaViewModel>>> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("O nome não pode ser nulo ou vazio.");
                }

                var baterias = await _bateriaService.GetByNameAsync(name);

                if (baterias.Any())
                {
                    return Ok(baterias);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Nenhuma pista encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pistas pelo nome: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BateriaViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _bateriaService.AddAsync(obj, loggedUser.Item3);
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
        public async Task<ActionResult> Update(int id, [FromBody] BateriaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _bateriaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _bateriaService.UpdateAsync(obj);
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
                    await _bateriaService.DeleteAsync(id);
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
