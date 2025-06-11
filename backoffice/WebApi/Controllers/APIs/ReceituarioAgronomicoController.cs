using Application.Application.Servicos.Log;
using Application.DTOs.Cadastros.ReceituarioAgronomico.Interface;
using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using Application.DTOs.Log.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
    public class ReceituarioAgronomicoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IReceituarioAgronomicoService _receituarioAgronomicoService;
        private readonly ILogService _loggerService;

        public ReceituarioAgronomicoController(
            LoggedUserInfoService loggedUserInfoService,
            IReceituarioAgronomicoService receituarioAgronomicoService,
            ILogService loggerService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _receituarioAgronomicoService = receituarioAgronomicoService;
            _loggerService = loggerService;
        }

        [HttpGet("{idRelatorioAplicacao}")]
        public async Task<ActionResult<IAsyncEnumerable<ReceituarioAgronomicoViewModel>>> GetAllByIdRelatorioAplicacaoAsync(int idRelatorioAplicacao)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();


                var obj = await _receituarioAgronomicoService.GetAllByIdRelatorioAplicacaoAsync(idRelatorioAplicacao);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao buscar receituarios da RelatorioAplicacao ID {idRelatorioAplicacao}: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar a receituarios: {ex.Message}").ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceituarioAgronomicoViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                

                var obj = await _receituarioAgronomicoService.GetByIdAsync(id);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao buscar a ReceituarioAgronomico com ID {id}: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar a ReceituarioAgronomico: {ex.Message}").ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ReceituarioAgronomicoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    await _receituarioAgronomicoService.AddAsync(obj);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao adicionar ReceituarioAgronomico: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao adicionar ReceituarioAgronomico: {ex.Message}").ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ReceituarioAgronomicoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _receituarioAgronomicoService.GetByIdAsync(id);
                    if (objeto is not null)
                    {
                        obj.Id = objeto.Id;
                        await _receituarioAgronomicoService.UpdateAsync(obj);
                        returnMsg.Clear();
                    }
                    else
                    {
                        returnMsg.Clear().Append("Não encontrado");
                    }
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao atualizar ReceituarioAgronomico: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao atualizar ReceituarioAgronomico: {ex.Message}").ToString());
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnMsg = new StringBuilder().Append("Solicitação não foi possível de ser executada");
            try
            {
                if (id != 0)
                {
                    await _receituarioAgronomicoService.DeleteAsync(id);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao deletar ReceituarioAgronomico: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao deletar ReceituarioAgronomico: {ex.Message}").ToString());
            }
        }

        [HttpGet("DownloadReceituario/{id}")]
        public async Task<IActionResult> DownloadReceituario(int id)
        {
            try
            {
                var receituario = await _receituarioAgronomicoService.GetByIdAsync(id);

                if (receituario.NomeArquivo != null && !string.IsNullOrEmpty(receituario.NomeArquivo.Data))
                {
                    var receituarioBytes = Convert.FromBase64String(receituario.NomeArquivo.Data);

                    // Nome do arquivo com base no formato
                    string receituarioFileName = receituario.Titulo;
                    string contentType = string.Empty;

                    switch (receituario.NomeArquivo.Format.ToLower())
                    {
                        case "pdf":
                            receituarioFileName += ".pdf";
                            contentType = "application/pdf";
                            break;
                        case "png":
                            receituarioFileName += ".png";
                            contentType = "image/png";
                            break;
                        case "raw":
                            receituarioFileName += ".png";
                            contentType = "image/png";
                            break;
                        default:
                            _loggerService.LogWarning($"Formato desconhecido: {receituario.NomeArquivo.Format}");
                            return BadRequest($"Formato desconhecido: {receituario.NomeArquivo.Format}");
                    }

                    // Retornar o arquivo diretamente
                    return File(receituarioBytes, contentType, receituarioFileName);
                }

                // Caso não haja dados no receituário
                return NotFound("Receituário não encontrado ou não possui dados.");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao recuperar o receituário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar o receituário: {ex.Message}");
            }
        }
    }
}
