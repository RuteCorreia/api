using Application.DTOs.Cadastros.Veiculo.Interface;
using Application.DTOs.Cadastros.Veiculo.ViewModel;
using Application.DTOs.Log.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
    public class VeiculoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IVeiculoService _veiculoService;
        private readonly ILogService _logService;

        public VeiculoController(
            LoggedUserInfoService loggedUserInfoService,
            IVeiculoService veiculoService,
            ILogService logService
        )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _veiculoService = veiculoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<VeiculoViewModel>>> GetAll()
        {
            var returnMsg = new StringBuilder().Append("Erro ao recuperar todos os veículos.");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var veiculos = await _veiculoService.GetAllAsync(loggedUser.Item3);
                _logService.LogInformation("Todos os veículos foram recuperados com sucesso.");
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VeiculoViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Erro ao recuperar veículo por ID.");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var veiculo = await _veiculoService.GetByIdAsync(id, loggedUser.Item3);
                if (veiculo != null)
                {
                    _logService.LogInformation("Veículo recuperado com sucesso.");
                    return Ok(veiculo);
                }

                _logService.LogWarning("Veículo não encontrado.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] VeiculoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Erro ao adicionar novo veículo.");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _veiculoService.AddAsync(obj, loggedUser.Item3);
                    _logService.LogInformation("Novo veículo adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo veículo.");
                return BadRequest("Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] VeiculoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Erro ao atualizar veículo.");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var veiculo = await _veiculoService.GetByIdAsync(id, loggedUser.Item3);
                    if (veiculo != null)
                    {
                        obj.Id = veiculo.Id;
                        await _veiculoService.UpdateAsync(obj);
                        _logService.LogInformation("Veículo atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Veículo não encontrado para atualização.");
                        return NotFound();
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar veículo.");
                return BadRequest("Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnMsg = new StringBuilder().Append("Erro ao excluir veículo.");
            try
            {
                if (id != 0)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _veiculoService.DeleteAsync(id, loggedUser.Item3);
                    _logService.LogInformation("Veículo excluído com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para excluir veículo.");
                return BadRequest("Solicitação inválida para excluir veículo");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }

        [HttpGet("kmAtual/{id:int}")]
        public async Task<ActionResult<int?>> GetKmAtualById(int id)
        {
            var returnMsg = new StringBuilder().Append("Erro ao recuperar o km atual do veículo por ID.");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var kmAtual = await _veiculoService.GetKmAtualByIdAsync(id, loggedUser.Item3);
                if (kmAtual.HasValue)
                {
                    _logService.LogInformation("Km atual do veículo recuperado com sucesso.");
                    return Ok(kmAtual);
                }

                _logService.LogWarning("Veículo não encontrado.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, returnMsg.Append($" Detalhes: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.ToString());
            }
        }
    }
}
