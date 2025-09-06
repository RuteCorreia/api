using Application.DTOs.Cadastros.Dashboard.Interface;
using Application.DTOs.Cadastros.Dashboard.ViewModel;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;
using System.IO.Compression;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            LoggedUserInfoService loggedUserInfoService,
            IDashboardService dashboardService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime? dataInicio, DateTime? dataFim, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetAllAsync(dataInicio, dataFim, loggedUser.Item3,usuario,nomeAeronave,nomeContratante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Dashboard graficos getAll - {ex.Message}");
            }
        }

        [HttpGet("gerar-excel-faturamento")]
        public async Task<IActionResult> GerarExcelFaturamento(DateTime? dataInicio, DateTime? dataFim, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetFaturamentoExportAsync(dataInicio, dataFim, loggedUser.Item3, usuario, nomeAeronave, nomeContratante);
                var dashboards = new List<ExportDashboardViewModel>();

                // Iterar sobre cada ID fornecido
                foreach (var item in result)
                {
                    var dashboard = new ExportDashboardViewModel
                    {
                        Relatorio = $"{item.TipoRelatorio} - {item.NumeroDocumento}",
                        Mes = new DateTime(1, item.Mes, 1).ToString("MMMM", new CultureInfo("pt-BR")),
                        Ano = item.Ano,
                        DataCriacao = item.DataCriacao,
                        Aeronave = item.Aeronave,
                        Piloto = item.Piloto,
                        Executor = item.Executor,
                        Cliente = item.Cliente,
                        HectaresVoados = item.ExtensaoTotal,
                        Faturamento = item.ValorTotal
                    };
                    dashboards.Add(dashboard);
                }

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    // Adicionar uma planilha ao arquivo Excel
                    var worksheet = package.Workbook.Worksheets.Add("Relatórios");

                    // Definir cabeçalhos das colunas
                    worksheet.Cells[1, 1].Value = "Documento";
                    worksheet.Cells[1, 2].Value = "Mes";
                    worksheet.Cells[1, 3].Value = "Ano";
                    worksheet.Cells[1, 4].Value = "Data Executada";
                    worksheet.Cells[1, 5].Value = "Aeronave";
                    worksheet.Cells[1, 6].Value = "Piloto";
                    worksheet.Cells[1, 7].Value = "Executor";
                    worksheet.Cells[1, 8].Value = "Cliente";
                    worksheet.Cells[1, 9].Value = "Hectares Voados";
                    worksheet.Cells[1, 10].Value = "Faturamento";

                    // Preencher dados
                    int row = 2;
                    foreach (var item in dashboards)
                    {

                        // Formatar Faturamento como moeda R$
                        worksheet.Cells[row, 10].Style.Numberformat.Format = "R$ #,##0.00";
                        worksheet.Cells[row, 10].Value = item.Faturamento;

                        // Formatar Hectares Voados como "xx.xxx ha"
                        worksheet.Cells[row, 9].Style.Numberformat.Format = "0 \"ha\"";
                        worksheet.Cells[row, 9].Value = item.HectaresVoados;

                        // Formatar Horas Voadas como "hh:mm:ss"
                        //var horasVoadasDecimal = item.HorasVoadas;
                        //int horas = (int)horasVoadasDecimal;
                        //int minutos = (int)((horasVoadasDecimal - horas) * 60);
                        //int segundos = (int)(((horasVoadasDecimal - horas) * 60 - minutos) * 60);
                        //worksheet.Cells[row, 10].Value = $"{horas:D2}:{minutos:D2}:{segundos:D2}";

                        // Formatar Rendimento como porcentagem
                        //worksheet.Cells[row, 11].Value = item.Rendimento;
                        //worksheet.Cells[row, 11].Style.Numberformat.Format = "0.00";

                        // Preencher outras células
                        worksheet.Cells[row, 1].Value = item.Relatorio;
                        worksheet.Cells[row, 2].Value = item.Mes;
                        worksheet.Cells[row, 3].Value = item.Ano;
                        worksheet.Cells[row, 4].Value = item.DataCriacao?.ToString("dd/MM/yyyy");
                        worksheet.Cells[row, 5].Value = item.Aeronave;
                        worksheet.Cells[row, 6].Value = item.Piloto;
                        worksheet.Cells[row, 7].Value = item.Executor;
                        worksheet.Cells[row, 8].Value = item.Cliente;

                        row++;
                    }

                    // Ajustar o estilo das células, se necessário
                    worksheet.Cells[1, 1, row - 1, 10].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row - 1, 10].AutoFitColumns();

                    package.Save();
                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Planilha_Faturamento.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Export Excel Faturamento - {ex.Message}");
            }
        }

        [HttpGet("gerar-excel-rendimento")]
        public async Task<IActionResult> GerarExcelRendimento(DateTime? dataInicio, DateTime? dataFim, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetRendimentoExportAsync(dataInicio, dataFim, loggedUser.Item3, usuario, nomeAeronave, nomeContratante);
                var dashboards = new List<ExportDashboardViewModel>();

                // Iterar sobre cada ID fornecido
                foreach (var item in result)
                {
                    var dashboard = new ExportDashboardViewModel
                    {
                        Relatorio = item.NumeroDocumento,
                        Mes = new DateTime(1, item.Mes, 1).ToString("MMMM", new CultureInfo("pt-BR")),
                        Ano = item.Ano,
                        DataCriacao = item.DataCriacao,
                        Aeronave = item.Aeronave,
                        Piloto = item.Piloto,
                        Executor = item.Executor,
                        HectaresVoados = item.ExtensaoTotal,
                        HorasVoadas = item.TotalHoras,
                        TotalHorasAplicacao = item.TotalHorasAplicacao,
                        TotalHorasIncendio = item.TotalHorasIncendio,
                        TotalHorasTranslado = item.TotalHorasTranslado,
                        Rendimento = item.Rendimento,
                    };
                    dashboards.Add(dashboard);
                }

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    // Adicionar uma planilha ao arquivo Excel
                    var worksheet = package.Workbook.Worksheets.Add("Relatórios");

                    // Definir cabeçalhos das colunas
                    worksheet.Cells[1, 1].Value = "Documento";
                    worksheet.Cells[1, 2].Value = "Mes";
                    worksheet.Cells[1, 3].Value = "Ano";
                    worksheet.Cells[1, 4].Value = "Data Executada";
                    worksheet.Cells[1, 5].Value = "Aeronave";
                    worksheet.Cells[1, 6].Value = "Piloto";
                    worksheet.Cells[1, 7].Value = "Executor";
                    worksheet.Cells[1, 8].Value = "Hectares Voados";
                    worksheet.Cells[1, 9].Value = "Horas Voadas Total (TR+SA)";
                    worksheet.Cells[1, 10].Value = "Horas Voadas SA";
                    worksheet.Cells[1, 11].Value = "Horas Voadas TR";
                    worksheet.Cells[1, 12].Value = "Horas Voadas IN";
                    worksheet.Cells[1, 13].Value = "Rendimento Total";
                    worksheet.Cells[1, 14].Value = "Rendimento SA";

                    // Preencher dados
                    int row = 2;
                    foreach (var item in dashboards)
                    {
                        // Formatar Hectares Voados como "xx.xxx ha"
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "0 \"ha\"";
                        worksheet.Cells[row, 8].Value = item.HectaresVoados;

                        // Formatar Horas Aplicacao como "hh:mm:ss"
                        decimal horasAplicacao = item.TotalHorasAplicacao.HasValue ? Convert.ToDecimal(item.TotalHorasAplicacao.Value) : 0m;

                        // Formatar Horas Translado como "hh:mm:ss"
                        decimal horasTranslado = item.TotalHorasTranslado.HasValue ? Convert.ToDecimal(item.TotalHorasTranslado.Value) : 0m;

                        // Formatar Horas Incendio como "hh:mm:ss"
                        decimal horasIncendio = item.TotalHorasIncendio.HasValue ? Convert.ToDecimal(item.TotalHorasIncendio.Value) : 0m;

                        TimeSpan tsAplicacao = TimeSpan.FromHours((double)horasAplicacao);
                        TimeSpan tsTranslado = TimeSpan.FromHours((double)horasTranslado);
                        TimeSpan tsIncendio = TimeSpan.FromHours((double)horasIncendio);

                        TimeSpan tsTotal = tsAplicacao + tsTranslado + tsIncendio;

                        worksheet.Cells[row, 10].Value = FormatarHoras((decimal)tsAplicacao.TotalHours);
                        worksheet.Cells[row, 11].Value = FormatarHoras((decimal)tsTranslado.TotalHours);
                        worksheet.Cells[row, 12].Value = FormatarHoras((decimal)tsIncendio.TotalHours);
                        worksheet.Cells[row, 9].Value = FormatarHoras((decimal)tsTotal.TotalHours);

                        // Formatar Rendimento como porcentagem
                        worksheet.Cells[row, 13].Style.Numberformat.Format = "0.00 \"ha/hr\"";
                        worksheet.Cells[row, 13].Value = (decimal)tsTotal.TotalHours > 0 ? item.HectaresVoados / (decimal)tsTotal.TotalHours : "";

                        // Formatar Rendimento como porcentagem
                        worksheet.Cells[row, 14].Style.Numberformat.Format = "0.00 \"ha/hr\"";
                        worksheet.Cells[row, 14].Value = horasAplicacao > 0 ? item.HectaresVoados / horasAplicacao : "";

                        // Preencher outras células
                        worksheet.Cells[row, 1].Value = item.Relatorio;
                        worksheet.Cells[row, 2].Value = item.Mes;
                        worksheet.Cells[row, 3].Value = item.Ano;
                        worksheet.Cells[row, 4].Value = item.DataCriacao?.ToString("dd/MM/yyyy");
                        worksheet.Cells[row, 5].Value = item.Aeronave;
                        worksheet.Cells[row, 6].Value = item.Piloto;
                        worksheet.Cells[row, 7].Value = item.Executor;

                        row++;
                    }

                    // Ajustar o estilo das células, se necessário
                    worksheet.Cells[1, 1, row - 1, 14].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row - 1, 14].AutoFitColumns();

                    package.Save();
                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Planilha_Rendimento.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Export Excel Faturamento - {ex.Message}");
            }
        }

        private string FormatarHoras(decimal horasDecimais)
        {
            TimeSpan tempo = TimeSpan.FromHours((double)horasDecimais);
            return $"{(int)tempo.TotalHours:D2}:{tempo.Minutes:D2}:{tempo.Seconds:D2}";
        }


        [HttpGet("getUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetUsuariosDropdownAsync(loggedUser.Item3);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("getClientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetClientesDropdownAsync(loggedUser.Item3);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("getAeronaves")]
        public async Task<IActionResult> GetAeronaves()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetAeronavesDropdownAsync(loggedUser.Item3);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }
    }
}
