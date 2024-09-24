using Application.DTOs.Cadastros.FrotaBateria.Interface;
using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.Interface;
using Application.DTOs.Cadastros.Gerador.ViewModel;
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
    public class FrotaBateriaController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IFrotaBateriaService _frotaBateriaService;
        public FrotaBateriaController(
            LoggedUserInfoService loggedUserInfoService,
            IFrotaBateriaService frotaBateriaService
            )
        {
            _loggedUserInfoService = loggedUserInfoService;
            _frotaBateriaService = frotaBateriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<FrotaBateriaViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var frotaBaterias = await _frotaBateriaService.GetAllAsync(loggedUser.Item3);
                return Ok(frotaBaterias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FrotaBateriaViewModel>> GetById(int id)
        {
            try
            {
                var frotaBateria = await _frotaBateriaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(frotaBateria))
                {
                    return Ok(frotaBateria);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FrotaBateriaViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var id = await _frotaBateriaService.AddAsync(obj, loggedUser.Item3);
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
        public async Task<ActionResult> Update([FromBody] FrotaBateriaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ObjectNullValidation.IsObjectNull(obj))
                    {
                        var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                        await _frotaBateriaService.UpdateAsync(obj);
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
                    await _frotaBateriaService.DeleteAsync(id);
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
