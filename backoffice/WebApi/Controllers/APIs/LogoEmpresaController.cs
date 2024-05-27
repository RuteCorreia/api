using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Application.DTOs.Log.Interface;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LogoEmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly ILogService _logService;

        public LogoEmpresaController(IEmpresaService empresaService, ILogService logService)
        {
            _empresaService = empresaService;
            _logService = logService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpresaViewModel>> GetLogoById(int id)
        {
            try
            {
                var logo = await _empresaService.GetLogoByIdAsync(id);

                if (!ObjectNullValidation.IsObjectNull(logo))
                {
                    _logService.LogInformation($"Logo da empresa com ID {id} obtido com sucesso.");
                    return Ok(logo);
                }

                _logService.LogInformation($"Logo da empresa com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter logo da empresa com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter logo da empresa com ID {id}: {ex.Message}");
            }
        }
    }
}
