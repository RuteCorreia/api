using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
//[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<EmpresaViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _empresaService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpresaViewModel>> GetById(int id)
    {
        try
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if(empresa.Imagem != null)
            {
                var base64Imagem = Convert.ToBase64String(empresa.Imagem);
                var base64Append = "data:image/jpeg;base64," + base64Imagem;
                empresa.ImagemBase64 = base64Append;
                
            }

            if (!ObjectNullValidation.IsObjectNull(empresa))
            {
                return Ok(empresa);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] EmpresaViewModel empresaViewModel)
    {
        try
        {
            if(!string.IsNullOrEmpty(empresaViewModel.ImagemBase64))
            {
                string[] parts = empresaViewModel.ImagemBase64.Split(',');
                string decodedBase64String = parts[1];

                byte[] imageDataBytes = Convert.FromBase64String(decodedBase64String);
                empresaViewModel.Imagem = imageDataBytes;
            }

            if (ModelState.IsValid)
            {
                await _empresaService.AddAsync(empresaViewModel);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] EmpresaViewModel empresaViewModel)
    {
        try
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _empresaService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Empresa delete - {ex.Message}");
        }
    }
}
