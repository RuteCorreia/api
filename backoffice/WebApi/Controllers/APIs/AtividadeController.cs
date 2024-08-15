using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Atividade.Interface;
using Application.DTOs.Cadastros.Atividade.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeService _atividadeService;
        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpGet("GetByContrante")]
        public async Task<ActionResult<AtividadeViewModel>> GetByContrante(string contratante)
        {
            try
            {
                var atividade = await _atividadeService.GetAtividadeByContratanteAsync(contratante);
                return Ok(atividade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("GetByPiloto")]
        public async Task<ActionResult<AtividadeViewModel>> GetByPiloto(string piloto)
        {
            try
            {
                var atividade = await _atividadeService.GetAtividadeByPilotoAsync(piloto);
                return Ok(atividade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("GetByExecutor")]
        public async Task<ActionResult<AtividadeViewModel>> GetByExecutor(string executor)
        {
            try
            {
                var atividade = await _atividadeService.GetAtividadeByExecutorAsync(executor);
                return Ok(atividade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("GetByPrefixo")]
        public async Task<ActionResult<AtividadeViewModel>> GetByPrefixo(string prefixo)
        {
            try
            {
                var atividade = await _atividadeService.GetAtividadeByPrefixoAsync(prefixo);
                return Ok(atividade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }
    }
}
