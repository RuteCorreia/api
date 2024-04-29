using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class IdentificacaoAreaTratadaController : ControllerBase
    {
        private readonly IIdentificacaoAreaTratadaService _identificacaoAreaTratadaService;

        public IdentificacaoAreaTratadaController(IIdentificacaoAreaTratadaService identificacaoAreaTratadaService)
        {
            _identificacaoAreaTratadaService = identificacaoAreaTratadaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<IdentificacaoAreaTratadaViewModel>>> GetAll()
        {
            try
            {
                var identificacaoAreaTratada = await _identificacaoAreaTratadaService.GetAllAsync();
                return Ok(identificacaoAreaTratada);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"IdentificacaoAreaTratada getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IdentificacaoAreaTratadaViewModel>> GetById(int id)
        {
            try
            {
                var identificacaoAreaTratada = await _identificacaoAreaTratadaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(identificacaoAreaTratada))
                {
                    return Ok(identificacaoAreaTratada);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"IdentificacaoAreaTratada getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] IdentificacaoAreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identificacaoAreaTratadaService.AddAsync(obj);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"IdentificacaoAreaTratada add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] IdentificacaoAreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _identificacaoAreaTratadaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _identificacaoAreaTratadaService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"IdentificacaoAreaTratada update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _identificacaoAreaTratadaService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"IdentificacaoAreaTratada delete - {ex.Message}");
            }
        }
    }
}
