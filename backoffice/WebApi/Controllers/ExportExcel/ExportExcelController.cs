using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.ExportExcel.Interfaces;
using Application.DTOs.ExportExcel.ViewModel;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Domain.Entidades.Export_Excel;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO.Compression;
using WebApi.HttpRequestInfo;

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
        private readonly IExportacaoPlanilhaService _exportacaoPlanilhaService;
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        private readonly ICombateIncendioService _combateIncendioService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        public ExportExcelController(
            IExportacaoPlanilhaService exportacaoPlanilhaService,
            IRelatorioAplicacaoService relatorioAplicacaoService,
            ICombateIncendioService combateIncendioService,
            LoggedUserInfoService loggedUserInfoService)
        {
            _exportacaoPlanilhaService = exportacaoPlanilhaService;
            _relatorioAplicacaoService = relatorioAplicacaoService;
            _combateIncendioService = combateIncendioService;
            _loggedUserInfoService = loggedUserInfoService;
        }

        [HttpPost("exportRelatorioApliacaoEIncendio/{nomeZip}")]
        public async Task<IActionResult> ExportExcel([FromBody] List<RelatorioInfoViewModel> relatorioInfo, string nomeZip)
        {
            try
            {
                if (relatorioInfo == null || relatorioInfo.Count == 0)
                {
                    return BadRequest("Nenhum ID fornecido para exportação.");
                }

                var relatorios = new List<ExportRelatorioViewModel>();

                // Iterar sobre cada ID fornecido
                foreach (var relatorio in relatorioInfo)
                {
                    if (relatorio.NomeRelatorio.StartsWith("Aplicação"))
                    {
                        var relatorioAplicacao = await _relatorioAplicacaoService.ExportExcelAsync(relatorio.Id);
                        await _relatorioAplicacaoService.UpdateDataAlteracaoAsync(relatorio.Id);
                        relatorios.Add(relatorioAplicacao);
                    }
                    else if(relatorio.NomeRelatorio.StartsWith("Combate Incendio"))
                    {
                        var relatorioIncendio = await _combateIncendioService.ExportExcelAsync(relatorio.Id);
                        await _combateIncendioService.UpdateDataAlteracaoAsync(relatorio.Id);
                        relatorios.Add(relatorioIncendio);
                    }
                    
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
                    worksheet.Cells[1, 6].Value = "HORAS APLICAÇÃO";
                    worksheet.Cells[1, 7].Value = "CULTURA";
                    worksheet.Cells[1, 8].Value = "TIPO DE SERVIÇO";
                    worksheet.Cells[1, 9].Value = "CLASSE AGROTÓXICOS";
                    worksheet.Cells[1, 10].Value = "ÁREA (ha)";
                    worksheet.Cells[1, 11].Value = "AGROTÓXICO";
                    worksheet.Cells[1, 12].Value = "FERTILIZANTES/ADJUVANTES/OUTROS";
                    worksheet.Cells[1, 13].Value = "SEMEADURA";
                    worksheet.Cells[1, 14].Value = "COMBATE A INCÊNDIO (HORAS)";
                    worksheet.Cells[1, 15].Value = "COMBATE A INCÊNDIO (HORAS)";
                    worksheet.Cells[1, 16].Value = "VOLUME (l/ha)";
                    worksheet.Cells[1, 17].Value = "DOSAGEM";
                    worksheet.Cells[1, 18].Value = "Unidade";

                    // Preencher dados
                    int row = 2;
                    foreach (var relatorio in relatorios)
                    {
                        TimeSpan timeSpanAplicacao;
                        TimeSpan timeSpanIncendio;

                        string horasAplicacaoFormatado = "";
                        string horasIncendioFormatado = "";

                        if (!string.IsNullOrEmpty(relatorio.HorasAplicacao))
                        {
                            int horasAplicacao = int.Parse(relatorio.HorasAplicacao.Substring(0, relatorio.HorasAplicacao.Length - 2));
                            int minutosAplicacao = int.Parse(relatorio.HorasAplicacao.Substring(relatorio.HorasAplicacao.Length - 2, 2));

                            timeSpanAplicacao = new TimeSpan(horasAplicacao, minutosAplicacao, 0);

                            horasAplicacaoFormatado = timeSpanAplicacao.ToString(@"hh\:mm\:ss");
                        }

                        if (!string.IsNullOrEmpty(relatorio.HorasCombateIncendio))
                        {
                            int horasIncendio = int.Parse(relatorio.HorasCombateIncendio.Substring(0, relatorio.HorasCombateIncendio.Length - 2));
                            int minutosIncendio = int.Parse(relatorio.HorasCombateIncendio.Substring(relatorio.HorasCombateIncendio.Length - 2, 2));

                            timeSpanIncendio = new TimeSpan(horasIncendio, minutosIncendio, 0);

                            horasIncendioFormatado = timeSpanIncendio.ToString(@"hh\:mm\:ss");
                        }                         

                        // Preencher células
                        worksheet.Cells[row, 1].Value = relatorio.UF;
                        worksheet.Cells[row, 2].Value = relatorio.Municipio;
                        worksheet.Cells[row, 3].Value = relatorio.TipoAeronave;
                        worksheet.Cells[row, 4].Value = relatorio.PrefixoAeronave;
                        worksheet.Cells[row, 5].Value = relatorio.HorasAplicacao;
                        worksheet.Cells[row, 6].Value = horasAplicacaoFormatado;
                        worksheet.Cells[row, 7].Value = relatorio.Cultura;
                        worksheet.Cells[row, 8].Value = relatorio.TipoDeServico;
                        worksheet.Cells[row, 9].Value = relatorio.ClasseAgrotoxico;
                        worksheet.Cells[row, 10].Value = relatorio.Area;
                        worksheet.Cells[row, 11].Value = relatorio.Agrotoxico;
                        worksheet.Cells[row, 12].Value = relatorio.Adjuvante;
                        worksheet.Cells[row, 13].Value = relatorio.Semeadura;
                        worksheet.Cells[row, 14].Value = relatorio.HorasCombateIncendio;
                        worksheet.Cells[row, 15].Value = horasIncendioFormatado;
                        worksheet.Cells[row, 16].Value = relatorio.Volume;
                        worksheet.Cells[row, 17].Value = relatorio.Dosagem;
                        worksheet.Cells[row, 18].Value = relatorio.Unidade;
                        row++;
                    }

                    // Ajustar o estilo das células, se necessário
                    worksheet.Cells[1, 1, row - 1, 18].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row - 1, 18].AutoFitColumns();

                    package.Save();
                }

                var zipStream = new MemoryStream();
                using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var zipEntry = zip.CreateEntry($"Relatorio Mensal.xlsx", System.IO.Compression.CompressionLevel.Fastest);
                    using (var entryStream = zipEntry.Open())
                    {
                        stream.Position = 0;
                        await stream.CopyToAsync(entryStream);
                    }
                }
                

                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

                var existingFile = await _exportacaoPlanilhaService.GetFileByNameAsync(loggedUser.Item3,nomeZip);
                if (existingFile != null)
                {
                    zipStream.Position = 0;
                    await _exportacaoPlanilhaService.UpdateAsync(existingFile, zipStream);
                }
                else
                {
                    zipStream.Position = 0;
                    var arquivoZipId = await _exportacaoPlanilhaService.AddAsync(zipStream, nomeZip, loggedUser.Item3);
                }
                // Preparar o stream para download
                zipStream.Position = 0;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao exportar realtorios: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var arquivosZip = await _exportacaoPlanilhaService.GetAllAsync(loggedUser.Item3);
                if (arquivosZip == null || !arquivosZip.Any())
                {
                    return NotFound("Nenhum arquivo encontrado.");
                }

                var arquivosZipViewModel = arquivosZip.Select(arquivo => new ArquivoZipViewModel
                {
                    Id = arquivo.Id,
                    Nome = arquivo.Nome,
                    DadosBase64 = arquivo.Dados,
                    DataAlteracao = arquivo.DataAlteracao.HasValue
                        ? arquivo.DataAlteracao.Value.ToString("dd/MM/yyyy HH:mm:ss")
                        : string.Empty
                }).ToList();

                return Ok(arquivosZipViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar arquivos zip: {ex.Message}");
            }
        }

    }
}
