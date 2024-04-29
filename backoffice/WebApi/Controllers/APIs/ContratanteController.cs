using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.Contratante.ViewModel;
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
    public class ContratanteController : ControllerBase
    {
        private readonly IContratanteService _contratanteService;

        public ContratanteController(IContratanteService contratanteService)
        {
            _contratanteService = contratanteService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ContratanteViewModel>>> GetAll()
        {
            try
            {
                var contratante = await _contratanteService.GetAllAsync();
                return Ok(contratante);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Contratante getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContratanteViewModel>> GetById(int id)
        {
            try
            {
                var contratante = await _contratanteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(contratante))
                {
                    return Ok(contratante);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Contratante getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ContratanteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contratanteService.AddAsync(obj);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Contratante add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ContratanteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _contratanteService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _contratanteService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Contratante update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _contratanteService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Contratante delete - {ex.Message}");
            }
        }
    }

}
