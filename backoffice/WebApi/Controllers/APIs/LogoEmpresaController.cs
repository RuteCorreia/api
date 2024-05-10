using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
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
    public class LogoEmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly LoggedUserInfoService _loggedUserInfoService;

        public LogoEmpresaController(IEmpresaService empresaService, LoggedUserInfoService loggedUserInfoService)
        {
            _empresaService = empresaService;
            _loggedUserInfoService = loggedUserInfoService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpresaViewModel>> GetLogoById(int id)
        {
            try
            {
                var logo = await _empresaService.GetLogoByIdAsync(id);

                if (!ObjectNullValidation.IsObjectNull(logo))
                    return Ok(logo);

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"LogoEmpresa GetLogoById - {ex.Message}");
            }
        }
    }
}
