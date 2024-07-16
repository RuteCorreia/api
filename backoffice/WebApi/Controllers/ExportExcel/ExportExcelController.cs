using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO.Compression;

namespace WebApi.Controllers.ExportExcel
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ExportExcelController : ControllerBase
    {
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        public ExportExcelController(IRelatorioAplicacaoService relatorioAplicacaoService)
        {
            _relatorioAplicacaoService = relatorioAplicacaoService;
        }

        [HttpGet("exportRelatorioApliacaoEIncendio")]
        public async Task<IActionResult> ExportExcel([FromQuery] List<int> ids)
        {
            try
            {
                if (ids == null || ids.Count == 0)
                {
                    return BadRequest("Nenhum ID fornecido para exportação.");
                }

                var relatorios = new List<RAExportExcelViewModel>();

                // Iterar sobre cada ID fornecido
                foreach (var id in ids)
                {
                    var relatorio = await _relatorioAplicacaoService.ExportExcelAsync(id);
                    relatorios.Add(relatorio);
                }

                // Criar um novo arquivo Excel
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    // Adicionar uma planilha ao arquivo Excel
                    var worksheet = package.Workbook.Worksheets.Add("Relatórios");

                    // Definir cabeçalhos das colunas
                    worksheet.Cells[1, 1].Value = "UF";
                    worksheet.Cells[1, 2].Value = "MUNICÍPIO";
                    worksheet.Cells[1, 3].Value = "TIPO AERONAVE";
                    worksheet.Cells[1, 4].Value = "PREFIXO AERONAVE";
                    worksheet.Cells[1, 5].Value = "HORAS APLICAÇÃO";
                    worksheet.Cells[1, 6].Value = "CULTURA";
                    worksheet.Cells[1, 7].Value = "TIPO DE SERVIÇO";
                    worksheet.Cells[1, 8].Value = "CLASSE AGROTÓXICOS";
                    worksheet.Cells[1, 9].Value = "ÁREA (ha)";
                    worksheet.Cells[1, 10].Value = "AGROTÓXICO";
                    worksheet.Cells[1, 11].Value = "FERTILIZANTES/ADJUVANTES/OUTROS";

                    // Preencher dados
                    int row = 2;
                    foreach (var relatorio in relatorios)
                    {
                        // Dividir o nome da aeronave em prefixo e tipo de aeronave
                        string[] partesNomeAeronave = relatorio.NomeAeronave.Split('-', StringSplitOptions.TrimEntries);
                        string prefixo = partesNomeAeronave[0].Trim();
                        string tipoAeronave = partesNomeAeronave.Length > 1 ? partesNomeAeronave[1].Trim() : "";

                        if (tipoAeronave == "AVIAO")
                        {
                            tipoAeronave = "Convencional";
                        }
                        else if (tipoAeronave == "DRONE")
                        {
                            tipoAeronave = "Drone";
                        }


                        // Preencher células
                        worksheet.Cells[row, 1].Value = relatorio.UF;
                        worksheet.Cells[row, 2].Value = relatorio.Cidade;
                        worksheet.Cells[row, 3].Value = tipoAeronave;
                        worksheet.Cells[row, 4].Value = prefixo;
                        worksheet.Cells[row, 5].Value = relatorio.HorasAplicacao;
                        worksheet.Cells[row, 6].Value = relatorio.Cultura;
                        worksheet.Cells[row, 7].Value = relatorio.TipoServico;
                        worksheet.Cells[row, 8].Value = relatorio.Classe;
                        worksheet.Cells[row, 9].Value = relatorio.TotalAreaAplicada;
                        worksheet.Cells[row, 10].Value = relatorio.NomeProduto;
                        worksheet.Cells[row, 11].Value = relatorio.Adjuvante;
                        row++;
                    }

                    // Ajustar o estilo das células, se necessário
                    worksheet.Cells[1, 1, row - 1, 11].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row - 1, 11].AutoFitColumns();

                    package.Save();
                }

                var zipStream = new MemoryStream();
                using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var zipEntry = zip.CreateEntry("relatorios.xlsx", System.IO.Compression.CompressionLevel.Fastest);
                    using (var entryStream = zipEntry.Open())
                    {
                        stream.Position = 0;
                        await stream.CopyToAsync(entryStream);
                    }
                }

                // Preparar o stream para download
                zipStream.Position = 0;
                string zipName = $"relatorios_{DateTime.Now:yyyyMMddHHmmss}.zip";
                return File(zipStream, "application/zip", zipName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }
    }
}
