using Application.DTOs.Cadastros.Produto.Interface;
using Application.DTOs.Cadastros.Produto.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogService _logService;

        public ProdutoController(IProdutoService produtoService, ILogService logService)
        {
            _produtoService = produtoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ProdutoViewModel>>> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetAllAsync();
                _logService.LogInformation("Lista de produtos recuperada com sucesso.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os produtos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os produtos: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoViewModel>> GetById(int id)
        {
            try
            {
                var produto = await _produtoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(produto))
                {
                    _logService.LogInformation("Produto recuperado com sucesso.");
                    return Ok(produto);
                }

                _logService.LogWarning("Produto não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Produto não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar produto pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar produto pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProdutoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _produtoService.AddAsync(obj);
                    _logService.LogInformation("Novo produto adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar novo produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo produto: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProdutoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _produtoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _produtoService.UpdateAsync(obj);
                        _logService.LogInformation("Produto atualizado com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning("Produto não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Produto não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar produto: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _produtoService.DeleteAsync(id);
                    _logService.LogInformation("Produto deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação inválida para deletar produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar produto: {ex.Message}");
            }
        }

        [HttpGet("classes")]
        public async Task<ActionResult<IEnumerable<string>>> GetClasses()
        {
            var classes = await _produtoService.GetClasses();
            return Ok(classes);
        }

        [HttpGet("nomes/{*classe}")]
        public async Task<ActionResult<IEnumerable<ProdutoNomeViewModel>>> GetNomes(string classe)
        {
            var decodedClasse = Uri.UnescapeDataString(classe);
            var nomes = await _produtoService.GetNomes(decodedClasse);
            return Ok(nomes);
        }
    }
}
