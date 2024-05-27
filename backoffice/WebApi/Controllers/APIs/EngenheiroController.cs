// EngenheiroController.cs

using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
    public class EngenheiroController : ControllerBase
    {
        private readonly IEngenheiroService _engenheiroService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _logService;

        public EngenheiroController(IEngenheiroService engenheiroService, LoggedUserInfoService loggedUserInfoService, ILogService logService)
        {
            _engenheiroService = engenheiroService;
            _loggedUserInfoService = loggedUserInfoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<EngenheiroViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var engenheiros = await _engenheiroService.GetAllAsync(loggedUser.Item3);
                _logService.LogInformation($"Lista de todos os engenheiros obtida por {loggedUser.Item1}.");
                return Ok(engenheiros);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os engenheiros: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os engenheiros: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EngenheiroViewModel>> GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var engenheiro = await _engenheiroService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(engenheiro))
                    {
                        _logService.LogInformation($"Detalhes do engenheiro com ID {id} obtidos por {loggedUser.Item1}.");
                        return Ok(engenheiro);
                    }
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes do engenheiro com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes do engenheiro com ID {id}: {ex.Message}");
            }
        }
    }
}
