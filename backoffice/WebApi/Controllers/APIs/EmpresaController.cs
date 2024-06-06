using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Application.DTOs.Log.Interface;
using Domain.Enums;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Administrador")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly ILogService _logService;

    public EmpresaController(IEmpresaService empresaService, LoggedUserInfoService loggedUserInfoService, ILogService logService)
    {
        _empresaService = empresaService;
        _loggedUserInfoService = loggedUserInfoService;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<EmpresaViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                var empresas = await _empresaService.GetAllAsync();
                _logService.LogInformation($"Listagem de todas as empresas realizada por {loggedUser.Item1}.");
                return Ok(empresas);
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter todas as empresas: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todas as empresas: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpresaViewModel>> GetById(int id)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                var empresa = await _empresaService.GetByIdAsync(id);
                if (empresa.Imagem != null)
                {
                    var base64Imagem = Convert.ToBase64String(empresa.Imagem);
                    var base64Append = "data:image/jpeg;base64," + base64Imagem;
                    empresa.ImagemBase64 = base64Append;

                }

                if (!ObjectNullValidation.IsObjectNull(empresa))
                    return Ok(empresa);

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter empresa por ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter empresa por ID {id}: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] EmpresaViewModel empresaViewModel)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                if (!string.IsNullOrEmpty(empresaViewModel.ImagemBase64))
                {
                    string[] parts = empresaViewModel.ImagemBase64.Split(',');
                    string decodedBase64String = parts[1];

                    byte[] imageDataBytes = Convert.FromBase64String(decodedBase64String);
                    empresaViewModel.Imagem = imageDataBytes;
                }

                if (ModelState.IsValid)
                {
                    await _empresaService.AddAsync(empresaViewModel);
                    _logService.LogInformation($"Empresa adicionada por {loggedUser.Item1}.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar empresa: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar empresa: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] EmpresaViewModel empresaViewModel)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                if (!string.IsNullOrEmpty(empresaViewModel.ImagemBase64))
                {
                    string[] parts = empresaViewModel.ImagemBase64.Split(',');
                    string decodedBase64String = parts[1];

                    byte[] imageDataBytes = Convert.FromBase64String(decodedBase64String);
                    empresaViewModel.Imagem = imageDataBytes;
                }
                if (ModelState.IsValid)
                {
                    var objeto = await _empresaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        empresaViewModel.IdEmpresa = objeto.IdEmpresa;

                        await _empresaService.UpdateAsync(empresaViewModel);
                        _logService.LogInformation($"Empresa com ID {id} atualizada por {loggedUser.Item1}.");
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar empresa com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar empresa com ID {id}: {ex.Message}");
        }
    }

    [HttpPatch("changeStatus/{id:int}")]
    public async Task<ActionResult> ChangeStatus(int id, [FromBody] ChangeStatusEmpresaViewModel status)
    {
        var returnMsg = new StringBuilder().Append("Algo deu errado");
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                await _empresaService.ChangeStatusAsync(id, status.Status);
                _logService.LogInformation($"Status da empresa com ID {id} alterado por {loggedUser.Item1}.");
                returnMsg.Clear();
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao alterar status da empresa com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar status da empresa com ID {id}: {ex.Message}");
        }
    }

    [HttpGet("getByName/{name}")]
    public async Task<ActionResult<IAsyncEnumerable<EmpresaViewModel>>> GetByName(string name)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                var empresas = await _empresaService.GetByNameAsync(name);
                foreach (var empresa in empresas)
                {
                    if (empresa.Imagem != null)
                    {
                        var base64Imagem = Convert.ToBase64String(empresa.Imagem);
                        var base64Append = "data:image/jpeg;base64," + base64Imagem;
                        empresa.ImagemBase64 = base64Append;

                    }
                }
                
                if (!ObjectNullValidation.IsObjectNull(empresas))
                    return Ok(empresas);

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa getByName - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                if (id != 0)
                {
                    await _empresaService.DeleteAsync(id);
                    _logService.LogInformation($"Empresa com ID {id} deletado por {loggedUser.Item1}.");
                    return Ok();
                }

                _logService.LogWarning($"Empresa com ID {id} não pode ser deletado por {loggedUser.Item1}.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar status da empresa com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa delete - {ex.Message}");
        }
    }
}
