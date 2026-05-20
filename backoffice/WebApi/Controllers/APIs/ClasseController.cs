using Application.DTOs.Cadastros.Classe.Interface;
using Application.DTOs.Cadastros.Classe.ViewModel;
using Application.DTOs.Log.Interface;
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
    public class ClasseController : ControllerBase
    {
        private readonly IClasseService _classeService;
        private readonly ILogService _logService;

        public ClasseController(IClasseService classeService, ILogService logService)
        {
            _classeService = classeService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClasseViewModel>>> GetByTipoServico([FromQuery] int tipoServico)
        {
            try
            {
                var classes = await _classeService.GetByTipoServicoAsync(tipoServico);
                _logService.LogInformation("Classes recuperadas com sucesso.");
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar classes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar classes: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClasseViewModel>> Add([FromBody] ClasseCreateViewModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logService.LogWarning("Modelo inválido ao adicionar classe.");
                    return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
                }

                if (string.IsNullOrWhiteSpace(obj.Descricao))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Nome da classe não pode ser vazio");
                }

                var duplicate = await _classeService.GetByTipoServicoAsync(obj.IdTipoDeServico);
                if (duplicate.Any(c => c.Descricao != null && c.Descricao.Trim().ToUpper() == obj.Descricao.Trim().ToUpper()))
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Já existe uma classe com esta descrição para o mesmo tipo de serviço");
                }

                var created = await _classeService.AddAsync(obj);
                _logService.LogInformation("Classe adicionada com sucesso.");
                return Ok(created);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar classe: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar classe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ClasseUpdateViewModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logService.LogWarning("Modelo inválido ao atualizar classe.");
                    return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
                }

                if (string.IsNullOrWhiteSpace(obj.Descricao))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Nome da classe não pode ser vazio");
                }

                var error = await _classeService.UpdateAsync(id, obj);
                if (error != null)
                {
                    if (error.Contains("não encontrada"))
                        return StatusCode(StatusCodes.Status404NotFound, error);
                    return StatusCode(StatusCodes.Status409Conflict, error);
                }

                _logService.LogInformation("Classe atualizada com sucesso.");
                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar classe: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar classe: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var error = await _classeService.DeleteAsync(id);
                if (error != null)
                {
                    if (error.Contains("não encontrada"))
                        return StatusCode(StatusCodes.Status404NotFound, error);
                    return StatusCode(StatusCodes.Status400BadRequest, error);
                }

                _logService.LogInformation("Classe excluída com sucesso.");
                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir classe: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir classe: {ex.Message}");
            }
        }
    }
}
