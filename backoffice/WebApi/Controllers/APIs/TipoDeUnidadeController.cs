using Application.Application.Servicos.Cadastros.TipoDeServico;
using Application.DTOs.Cadastros.TipoDeServico.Interface;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.Interface;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using Application.DTOs.Log.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class TipoDeUnidadeController : ControllerBase
    {
        private readonly ITipoDeUnidadeService _tipoDeUnidadeService;
        private readonly ILogService _logService;

        public TipoDeUnidadeController(ITipoDeUnidadeService tipoDeUnidadeService, ILogService logService)
        {
            _tipoDeUnidadeService = tipoDeUnidadeService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<TipoDeUnidadeViewModel>>> GetAll()
        {
            try
            {
                var tiposProduto = await _tipoDeUnidadeService.GetAllAsync();
                _logService.LogInformation("Todos os tipos de produto foram recuperados com sucesso.");
                return Ok(tiposProduto);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
            }
        }
    }
}
