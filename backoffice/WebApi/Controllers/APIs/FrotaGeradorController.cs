using Application.DTOs.Cadastros.FrotaBateria.Interface;
using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.FrotaGerador.Interface;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
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
    public class FrotaGeradorController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IFrotaGeradorService _frotaGeradorService;
        public FrotaGeradorController(
            LoggedUserInfoService loggedUserInfoService,
            IFrotaGeradorService frotaGeradorService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _frotaGeradorService = frotaGeradorService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<FrotaGeradorViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var frotaGeradores = await _frotaGeradorService.GetAllAsync(loggedUser.Item3);
                return Ok(frotaGeradores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FrotaGeradorViewModel>> GetById(int id)
        {
            try
            {
                var frotaGerador = await _frotaGeradorService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(frotaGerador))
                {
                    return Ok(frotaGerador);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FrotaGeradorViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _frotaGeradorService.AddAsync(obj, loggedUser.Item3);
                    return Ok(id);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico add - {ex.Message}");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] FrotaGeradorViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _frotaGeradorService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _frotaGeradorService.UpdateAsync(obj);
                        return Ok();
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
                    await _frotaGeradorService.DeleteAsync(id);
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
