using Application.DTOs.Cadastros.Componentes.Interface;
using Application.DTOs.Cadastros.Componentes.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ComponenteController : ControllerBase
    {
        private readonly IComponentesService _componenteService;

        public ComponenteController(IComponentesService componenteService)
        {
            _componenteService = componenteService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ComponentesViewModel>>> GetAll()
        {
            try
            {
                var componente = await _componenteService.GetAllAsync();
                return Ok(componente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Componentes getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComponentesViewModel>> GetById(int id)
        {
            try
            {
                var componente = await _componenteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(componente))
                {
                    return Ok(componente);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ComponentesViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _componenteService.AddAsync(obj);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Componente add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ComponentesViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _componenteService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _componenteService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Componente update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _componenteService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Componente delete - {ex.Message}");
            }
        }
    }

}
