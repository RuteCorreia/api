using Application.DTOs.Cadastros.Dashboard.Interface;
using Application.DTOs.Cadastros.Dashboard.ViewModel;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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
                //DateTime? dataInicioDt = null;
                //DateTime? dataFimDt = null;
                //if (!string.IsNullOrWhiteSpace(dataInicio))
                //{
                //    if (DateTime.TryParse(dataInicio, out DateTime parsedDataInicio))
                //    {
                //        dataInicioDt = parsedDataInicio;
                //    }
                //    else
                //    {
                //        return BadRequest("Formato inválido para dataInicio.");
                //    }
                //}

                //if (!string.IsNullOrWhiteSpace(dataFim))
                //{
                //    if (DateTime.TryParse(dataFim, out DateTime parsedDataFim))
                //    {
                //        dataFimDt = parsedDataFim;
                //    }
                //    else
                //    {
                //        return BadRequest("Formato inválido para dataFim.");
                //    }
                //}

                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetAllAsync(dataInicio, dataFim, loggedUser.Item3,usuario,nomeAeronave,nomeContratante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("gerar-excel")]
        public async Task<IActionResult> GerarExcel(DateTime? dataInicio, DateTime? dataFim, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetAllAsync(dataInicio, dataFim, loggedUser.Item3, usuario, nomeAeronave, nomeContratante);
                var dashboards = new List<ExportDashboardViewModel>();

                // Iterar sobre cada ID fornecido
                foreach (var item in result)
                {
                    var dashboard = new ExportDashboardViewModel
                    {
                        DataInicio = dataInicio,
                        DataFim = dataFim,
                        Usuario = usuario,
                        Aeronave = nomeAeronave,
                        Cliente = nomeContratante,
                        Mes = new DateTime(1, item.Mes, 1).ToString("MMMM"),
                        Ano = item.Ano,
                        Faturamento = item.ValorTotal,
                        HectaresVoados = item.ExtensaoTotal,
                        HorasVoadas = item.TotalHoras,
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
                    worksheet.Cells[1, 1].Value = "Data Inicio";
                    worksheet.Cells[1, 2].Value = "Data Fim";
                    worksheet.Cells[1, 3].Value = "Usuario";
                    worksheet.Cells[1, 4].Value = "Aeronave";
                    worksheet.Cells[1, 5].Value = "Cliente";
                    worksheet.Cells[1, 6].Value = "Mes";
                    worksheet.Cells[1, 7].Value = "Ano";
                    worksheet.Cells[1, 8].Value = "Faturamento";
                    worksheet.Cells[1, 9].Value = "Hectares Voados";
                    worksheet.Cells[1, 10].Value = "Horas Voadas";
                    worksheet.Cells[1, 11].Value = "Rendimento";

                    // Preencher dados
                    int row = 2;
                    foreach (var item in dashboards)
                    {

                        // Formatar Faturamento como moeda R$
                        worksheet.Cells[row, 8].Style.Numberformat.Format = "R$ #,##0.00";
                        worksheet.Cells[row, 8].Value = item.Faturamento;

                        // Formatar Hectares Voados como "xx.xxx ha"
                        worksheet.Cells[row, 9].Style.Numberformat.Format = "#,##0.### \"ha\"";
                        worksheet.Cells[row, 9].Value = item.HectaresVoados;

                        // Formatar Horas Voadas como "hh:mm:ss"
                        var horasVoadasDecimal = item.HorasVoadas;
                        int horas = (int)horasVoadasDecimal;
                        int minutos = (int)((horasVoadasDecimal - horas) * 60);
                        int segundos = (int)(((horasVoadasDecimal - horas) * 60 - minutos) * 60);
                        worksheet.Cells[row, 10].Value = $"{horas:D2}:{minutos:D2}:{segundos:D2}";

                        // Formatar Rendimento como porcentagem
                        worksheet.Cells[row, 11].Value = item.Rendimento;
                        worksheet.Cells[row, 11].Style.Numberformat.Format = "0.00";

                        // Preencher outras células
                        worksheet.Cells[row, 1].Value = item.DataInicio?.ToString("dd/MM/yyyy");
                        worksheet.Cells[row, 2].Value = item.DataFim?.ToString("dd/MM/yyyy");
                        worksheet.Cells[row, 3].Value = item.Usuario;
                        worksheet.Cells[row, 4].Value = item.Aeronave;
                        worksheet.Cells[row, 5].Value = item.Cliente;
                        worksheet.Cells[row, 6].Value = item.Mes;
                        worksheet.Cells[row, 7].Value = item.Ano;
                        row++;
                    }

                    // Ajustar o estilo das células, se necessário
                    worksheet.Cells[1, 1, row - 1, 11].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row - 1, 11].AutoFitColumns();

                    package.Save();
                }

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "relatorios.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
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
