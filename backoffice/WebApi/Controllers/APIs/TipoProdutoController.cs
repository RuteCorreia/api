using Application.DTOs.Cadastros.Tipo_Produto.Interface;
using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using Application.DTOs.Log.Interface;
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
    public class TipoProdutoController : ControllerBase
    {
        private readonly ITipoProdutoService _tipoProdutoService;
        private readonly ILogService _logService;

        public TipoProdutoController(ITipoProdutoService tipoProdutoService, ILogService logService)
        {
            _tipoProdutoService = tipoProdutoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<TipoProdutoViewModel>>> GetAll()
        {
            try
            {
                var tiposProduto = await _tipoProdutoService.GetAllAsync();
                _logService.LogInformation("Todos os tipos de produto foram recuperados com sucesso.");
                return Ok(tiposProduto);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoProdutoViewModel>> GetById(int id)
        {
            try
            {
                var tipoProduto = await _tipoProdutoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(tipoProduto))
                {
                    _logService.LogInformation("Tipo de produto recuperado com sucesso.");
                    return Ok(tipoProduto);
                }

                _logService.LogWarning("Tipo de produto não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Tipo de produto não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar tipo de produto pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar tipo de produto pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TipoProdutoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _tipoProdutoService.AddAsync(obj);
                    _logService.LogInformation("Novo tipo de produto adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar novo tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo tipo de produto: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] TipoProdutoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _tipoProdutoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _tipoProdutoService.UpdateAsync(obj);
                        _logService.LogInformation("Tipo de produto atualizado com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning("Tipo de produto não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Tipo de produto não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar tipo de produto: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _tipoProdutoService.DeleteAsync(id);
                    _logService.LogInformation("Tipo de produto deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação inválida para deletar tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar tipo de produto: {ex.Message}");
            }
        }
    }
}
