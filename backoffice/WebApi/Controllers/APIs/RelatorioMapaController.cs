using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.RelatorioMapa.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;
using Application.DTOs.Log.Interface;
using Domain.Entidades.Cadastros.Aplicacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class RelatorioMapaController : ControllerBase
    {
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        private readonly ICombateIncendioService _combateIncendioService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDataRelatorioService _dataRelatorioService;
        private readonly ILogService _loggerService;
        public RelatorioMapaController(
            IRelatorioAplicacaoService relatorioAplicacaoService,
            ICombateIncendioService combateIncendioService,
            LoggedUserInfoService loggedUserInfoService,
            IDataRelatorioService dataRelatorioService,
            ILogService loggerService)
        {
            _relatorioAplicacaoService = relatorioAplicacaoService;
            _combateIncendioService = combateIncendioService;
            _loggedUserInfoService = loggedUserInfoService;
            _dataRelatorioService = dataRelatorioService;
            _loggerService = loggerService;
        }
        [HttpPost("DownloadRelatoriosMes")]
        public async Task<IActionResult> DownloadRelatoriosMes([FromBody] RelatorioInfo relatoriosInfo)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var isMapa = 1;

                var idsIncendio = new List<int>();
                var idsAplicacao = new List<int>();

                // Classificando relatórios
                foreach (var relatorio in relatoriosInfo.RelatoriosInfo)
                {
                    if (relatorio.Id.HasValue)
                    {
                        if (relatorio.NomeRelatorio.Contains("Combate Incendio"))
                        {
                            idsIncendio.Add(relatorio.Id.Value);
                        }
                        else if (relatorio.NomeRelatorio.Contains("Aplicação"))
                        {
                            idsAplicacao.Add(relatorio.Id.Value);
                        }
                    }
                }

                var relatoriosIncendio = await _combateIncendioService.GetListByIdsAsync(loggedUser.Item3, idsIncendio, isMapa);
                var relatoriosAplicacao = await _relatorioAplicacaoService.GetListByIdsAsync(loggedUser.Item3, idsAplicacao, isMapa);

                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        // Adicionando relatórios de aplicação
                        foreach (var relatorio in relatoriosAplicacao)
                        {
                            var relatorioRequest = relatoriosInfo.RelatoriosInfo.FirstOrDefault(x => x.Id == relatorio.Id);

                            // Nome da pasta com base no NomeRelatorio
                            string folderName = relatorio.NomeRelatorio.Replace("/", "-").Replace("\\", "-");

                            // Adicionar o arquivo do relatório
                            var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);
                            if (!string.IsNullOrEmpty(data.Data))
                            {
                                var relatorioBytes = Convert.FromBase64String(data.Data);
                                var nomeArquivoRelatorio = $"{folderName}/{folderName}.pdf"; // Nome do arquivo igual ao da pasta
                                var entry = archive.CreateEntry(nomeArquivoRelatorio, CompressionLevel.Fastest);

                                using (var entryStream = entry.Open())
                                {
                                    entryStream.Write(relatorioBytes, 0, relatorioBytes.Length);
                                }
                            }
                            else
                            {
                                _loggerService.LogWarning($"O relatório de aplicação com IdData {relatorio.IdData} não possui dados válidos.");
                            }

                            // Adicionar o arquivo do ReceituarioAgronomico, se disponível
                            if (relatorioRequest?.ReceituarioAgronomico != null &&
                                !string.IsNullOrEmpty(relatorioRequest.ReceituarioAgronomico.Data))
                            {
                                var receituarioBytes = Convert.FromBase64String(relatorioRequest.ReceituarioAgronomico.Data);
                                string receituarioFileName = $"{folderName}/receituarioAgronomico"; // Nome do arquivo do receituário

                                // Verificar o formato do ReceituarioAgronomico
                                if (relatorioRequest.ReceituarioAgronomico.Format.ToLower() == "pdf")
                                {
                                    receituarioFileName += ".pdf";
                                }
                                else if (relatorioRequest.ReceituarioAgronomico.Format.ToLower() == "png")
                                {
                                    receituarioFileName += ".png";
                                }
                                else
                                {
                                    _loggerService.LogWarning($"Formato desconhecido: {relatorioRequest.ReceituarioAgronomico.Format}");
                                    continue; // Pula para o próximo relatório se o formato for desconhecido
                                }

                                var receituarioEntry = archive.CreateEntry(receituarioFileName, CompressionLevel.Fastest);

                                using (var entryStream = receituarioEntry.Open())
                                {
                                    entryStream.Write(receituarioBytes, 0, receituarioBytes.Length);
                                }
                            }
                        }

                        // Adicionando relatórios de combate a incêndio
                        foreach (var relatorio in relatoriosIncendio)
                        {
                            var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                            if (!string.IsNullOrEmpty(data.Data))
                            {
                                var relatorioBytes = Convert.FromBase64String(data.Data);
                                string folderName = relatorio.NomeRelatorio.Replace("/", "-").Replace("\\", "-");
                                var nomeArquivo = $"{folderName}/{folderName}.pdf";
                                var entry = archive.CreateEntry(nomeArquivo, CompressionLevel.Fastest);

                                using (var entryStream = entry.Open())
                                {
                                    entryStream.Write(relatorioBytes, 0, relatorioBytes.Length);
                                }
                            }
                            else
                            {
                                _loggerService.LogWarning($"O relatório de combate a incêndio com IdData {relatorio.IdData} não possui dados válidos.");
                            }
                        }
                    }

                    // Preparando o arquivo ZIP para download
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream.ToArray(), "application/zip", $"relatorios-mapa-{relatoriosInfo.Mes}-{relatoriosInfo.Ano}.zip");
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao recuperar e compactar os relatórios de combate a incêndio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar e compactar os relatórios de combate a incêndio: {ex.Message}");
            }
        }

    }
}
