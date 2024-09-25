using Application.DTOs.Pdf.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _seuServico;

        public PdfController(IPdfService seuServico)
        {
            _seuServico = seuServico;
        }

        [HttpPost("adicionar-marca-dagua")]
        public async Task<IActionResult> AdicionarMarcaDaguaCancelado(IFormFile arquivoPdf)
        {
            if (arquivoPdf == null || arquivoPdf.Length == 0)
            {
                return BadRequest("Arquivo PDF não fornecido.");
            }

            // Lê o arquivo PDF para um array de bytes
            byte[] pdfBytes;
            using (var memoryStream = new MemoryStream())
            {
                await arquivoPdf.CopyToAsync(memoryStream);
                pdfBytes = memoryStream.ToArray();
            }

            // Adiciona a marca d'água
            byte[] pdfComMarcaDagua = await _seuServico.AdicionarMarcaDaguaCanceladoAsync(pdfBytes);

            // Retorna o PDF modificado
            return File(pdfComMarcaDagua, "application/pdf", "relatorio_cancelado.pdf");
        }
    }
}
