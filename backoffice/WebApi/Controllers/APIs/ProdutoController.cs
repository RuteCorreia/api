using Application.DTOs.Cadastros.Produto.Interface;
using Application.DTOs.Cadastros.Produto.ViewModel;
using Application.DTOs.Log.Interface;
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
    public class ProdutoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IProdutoService _produtoService;
        private readonly ILogService _logService;

        public ProdutoController(
            LoggedUserInfoService loggedUserInfoService,
            IProdutoService produtoService, 
            ILogService logService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _produtoService = produtoService;
            _logService = logService;
        }

        [HttpGet("{*nomeProduto}")]
        public async Task<ActionResult<IAsyncEnumerable<ProdutoViewModel>>> GetAll(string? nomeProduto)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produtos = await _produtoService.GetAllAsync(nomeProduto, loggedUser.Item3);
                _logService.LogInformation("Lista de produtos recuperada com sucesso.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os produtos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os produtos: {ex.Message}");
            }
        }

        [HttpGet("GetAllApp")]
        public async Task<ActionResult<IAsyncEnumerable<ProdutoViewModel>>> GetAllApp(string? nomeProduto)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produtos = await _produtoService.GetAllAppAsync(nomeProduto, loggedUser.Item3);
                _logService.LogInformation("Lista de produtos recuperada com sucesso.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os produtos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os produtos: {ex.Message}");
            }
        }

        [HttpGet("GetByIdApp/{id:int}")]
        public async Task<ActionResult<ProdutoViewModel>> GetByIdApp(int id)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produto = await _produtoService.GetByIdAppAsync(id, loggedUser.Item3);
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoViewModel>> GetById(int id)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produto = await _produtoService.GetByIdAsync(id, loggedUser.Item3);
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

        [HttpGet("getByName/{*name}")]
        public async Task<ActionResult<ProdutoViewModel>> GetByName(string name)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produto = await _produtoService.GetByNameAsync(name, loggedUser.Item3);
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
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var verificaSeProdutoExistePeloNome = await _produtoService.GetByNameAsync(obj.Nome, loggedUser.Item3);
                    if (verificaSeProdutoExistePeloNome != null)
                    {
                        _logService.LogWarning("Tentativa de adição de um produto com nome duplicado"); // Registre um aviso de log
                        return StatusCode(StatusCodes.Status409Conflict, "Já existe um produto com esse nome!");
                    }
                    await _produtoService.AddAsync(obj, loggedUser.Item3);
                    _logService.LogInformation("Novo produto adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Produto ja cadastrado"))
                {
                    _logService.LogWarning($"Produto já existente: {ex.Message}");
                    return StatusCode(StatusCodes.Status409Conflict, "Produto já cadastrado.");
                }

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
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var objeto = await _produtoService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _produtoService.UpdateAsync(obj);
                        _logService.LogInformation("Produto atualizado com sucesso.");
                        return Ok();
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
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var objeto = await _produtoService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _produtoService.DeleteAsync(id, loggedUser.Item3);
                    _logService.LogInformation("Produto deletado com sucesso.");
                    return Ok();
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

        [HttpGet("getNomesByIds")]
        public async Task<ActionResult<IEnumerable<string>>> GetNomesByIds([FromQuery(Name = "ids")] List<int> ids)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var produtos = await _produtoService.GetNomesByIdsAsync(ids, loggedUser.Item3);
                _logService.LogInformation("Lista de produtos recuperada com sucesso.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os produtos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os produtos: {ex.Message}");
            }
        }

        [HttpGet("classes")]
        public async Task<ActionResult<IEnumerable<string>>> GetClasses()
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var classes = await _produtoService.GetClasses(loggedUser.Item3);
            return Ok(classes);
        }

        [HttpGet("nomes/{*classe}")]
        public async Task<ActionResult<IEnumerable<ProdutoNomeViewModel>>> GetNomes(string classe)
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var decodedClasse = Uri.UnescapeDataString(classe);
            var nomes = await _produtoService.GetNomes(decodedClasse, loggedUser.Item3);
            return Ok(nomes);
        }
    }
}
