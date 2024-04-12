using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ManutencaoAeronaveController : ControllerBase
    {
        private readonly IManutencaoAeronaveService _manutencaoAeronaveService;
        private readonly IAeronaveService _aeronaveService;

        public ManutencaoAeronaveController(IManutencaoAeronaveService manutencaoAeronaveService, IAeronaveService aeronaveService)
        {
            _manutencaoAeronaveService = manutencaoAeronaveService;
            _aeronaveService = aeronaveService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ManutencaoAeronaveViewModel>>> GetAll()
        {
            try
            {
                var manutencaoAeronave = await _manutencaoAeronaveService.GetAllAsync();
                var aeronaves = await _aeronaveService.GetAllAsync();
                foreach (var item in manutencaoAeronave)
                {
                    var buscaAeronave = aeronaves.FirstOrDefault(x => x.Id == item.IdAeronave);
                    if (buscaAeronave != null)
                    {
                        item.PrefixoAeronave = buscaAeronave.Prefixo;
                    }
                }
                return Ok(manutencaoAeronave);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ManutencaoAeronaveViewModel>> GetById(int id)
        {
            try
            {
                var manutencaoAeronave = await _manutencaoAeronaveService.GetByIdAsync(id);
                if (manutencaoAeronave.Documento != null)
                {
                    var base64Imagem = Convert.ToBase64String(manutencaoAeronave.Documento);
                    var base64Append = "data:image/jpeg;base64," + base64Imagem;
                    manutencaoAeronave.DocumentoBase64 = base64Append;

                }
                if (!ObjectNullValidation.IsObjectNull(manutencaoAeronave))
                {
                    return Ok(manutencaoAeronave);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ManutencaoAeronaveViewModel obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.DocumentoBase64))
                {
                    string[] parts = obj.DocumentoBase64.Split(',');
                    string decodedBase64String = parts[1];

                    byte[] imageDataBytes = Convert.FromBase64String(decodedBase64String);
                    obj.Documento = imageDataBytes;
                }

                if (ModelState.IsValid)
                {
                    await _manutencaoAeronaveService.AddAsync(obj);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ManutencaoAeronaveViewModel obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.DocumentoBase64))
                {
                    string[] parts = obj.DocumentoBase64.Split(',');
                    string decodedBase64String = parts[1];

                    byte[] imageDataBytes = Convert.FromBase64String(decodedBase64String);
                    obj.Documento = imageDataBytes;
                }

                if (ModelState.IsValid)
                {
                    var objeto = await _manutencaoAeronaveService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _manutencaoAeronaveService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _manutencaoAeronaveService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave delete - {ex.Message}");
            }
        }
    }

}
