using Application.Application.Servicos.Cadastros.FrotaGerador;
using Application.DTOs.Cadastros.FrotaGerador.Interface;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using Application.DTOs.Cadastros.FrotaMotobomba.Interface;
using Application.DTOs.Cadastros.FrotaMotobomba.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class FrotaMotobombaController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IFrotaMotobombaService _frotaMotobombaService;
        public FrotaMotobombaController(
            LoggedUserInfoService loggedUserInfoService,
            IFrotaMotobombaService frotaMotobombaService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _frotaMotobombaService = frotaMotobombaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<FrotaMotobombaViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var frotaMotobombas = await _frotaMotobombaService.GetAllAsync(loggedUser.Item3);
                return Ok(frotaMotobombas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FrotaMotobombaViewModel>> GetById(int id)
        {
            try
            {
                var frotaMotobomba = await _frotaMotobombaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(frotaMotobomba))
                {
                    return Ok(frotaMotobomba);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FrotaMotobombaViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _frotaMotobombaService.AddAsync(obj, loggedUser.Item3);
                    return Ok(id);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico add - {ex.Message}");
            }
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromBody] FrotaMotobombaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ObjectNullValidation.IsObjectNull(obj))
                    {
                        var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                        var id = await _frotaMotobombaService.UpdateAsync(obj);
                        return Ok(id);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _frotaMotobombaService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico delete - {ex.Message}");
            }
        }
    }
}
