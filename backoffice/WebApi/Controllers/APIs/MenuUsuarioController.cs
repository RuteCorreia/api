using Application.DTOs.Cadastros.MenuUsuario.Interface;
using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
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
    public class MenuUsuarioController : ControllerBase
    {
        private readonly IMenuUsuarioService _menuUsuarioService;
        private readonly ILogService _logService;

        public MenuUsuarioController(IMenuUsuarioService menuUsuarioService, ILogService logService)
        {
            _menuUsuarioService = menuUsuarioService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<MenuUsuarioViewModel>>> GetAll()
        {
            try
            {
                var menuUsuario = await _menuUsuarioService.GetAllAsync();
                _logService.LogInformation("Registros de menu de usuário recuperados com sucesso.");
                return Ok(menuUsuario);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os registros de menu de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os registros de menu de usuário: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuUsuarioViewModel>> GetById(string id)
        {
            try
            {
                var menuUsuario = await _menuUsuarioService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(menuUsuario))
                {
                    return Ok(menuUsuario);
                }

                _logService.LogWarning("Menu de usuário não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Menu de usuário não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar menu de usuário pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar menu de usuário pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] MenuUsuarioViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _menuUsuarioService.AddAsync(obj);
                    _logService.LogInformation("Menu de usuário adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo de menu de usuário inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo de menu de usuário inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar menu de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar menu de usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _menuUsuarioService.DeleteAsync(id);
                    _logService.LogInformation("Menu de usuário excluído com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir menu de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir menu de usuário: {ex.Message}");
            }
        }
    }
}
