using Application.DTOs.ExportExcel.Interfaces;
using Domain.Entidades.Export_Excel;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Export_Excel;
using Helpers;

namespace Application.Application.Servicos.Export_Excel
{
    public class ExportacaoPlanilhaService : IExportacaoPlanilhaService
    {
        private readonly IExportacaoPlanilhaRepository _exportacaoPlanilhaRepository;
        private readonly IBlobStorageRepository _blobStorageRepository;
        public ExportacaoPlanilhaService(
            IExportacaoPlanilhaRepository exportacaoPlanilhaRepository,
            IBlobStorageRepository blobStorageRepository)
        {
            _exportacaoPlanilhaRepository = exportacaoPlanilhaRepository;
            _blobStorageRepository = blobStorageRepository;
        }
        public async Task<int> AddAsync(MemoryStream zipStream, string nomeArquivo, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var arquivoZip = new PlanilhaExcelExportada();
            if (zipStream != null)
            {

                string fileName = $"RelatorioAtividades - {Guid.NewGuid()}.zip";
                await _blobStorageRepository.SavePdfAsync(zipStream, fileName);

                arquivoZip.Dados = fileName;
            }
            arquivoZip.Nome = nomeArquivo;
            arquivoZip.DataCriacao = DateTime.Now;
            arquivoZip.DataAlteracao = DateTime.Now;
            arquivoZip.IdEmpresa = idEmpresaInt;

            return await _exportacaoPlanilhaRepository.AddAsync(arquivoZip);
        }

        public async Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync(string? idEmpresa)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                var planilhaExcelExportadas = await _exportacaoPlanilhaRepository.GetAllAsync(idEmpresaInt);
                foreach (var item in planilhaExcelExportadas) 
                {
                    var planilhaStream = await _blobStorageRepository.GetPdfAsync(item.Dados);
                    using (var memoryStream = new MemoryStream())
                    {
                        await planilhaStream.CopyToAsync(memoryStream);
                        var byteArray = memoryStream.ToArray();
                        item.Dados = Convert.ToBase64String(byteArray);
                    }
                }
                return planilhaExcelExportadas;
            }
            catch (Exception ex)
            {
                // Aqui você pode adicionar tratamento de exceção, logging, etc.
                throw new Exception("Erro ao obter Planilhas Exportadas.", ex);
            }
        }

        public async Task<PlanilhaExcelExportada> GetFileByNameAsync(string? idEmpresa,string name)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                var planilhas = await _exportacaoPlanilhaRepository.GetFileByNameAsync(idEmpresaInt, name);
                if (planilhas != null) 
                {
                    return planilhas;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Aqui você pode adicionar tratamento de exceção, logging, etc.
                throw new Exception("Erro ao obter Planilhas Exportadas.", ex);
            }
        }

        public async Task UpdateAsync(PlanilhaExcelExportada obj, MemoryStream zipStream)
        {
            TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");
            DateTime dataAlteracao = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);
            if (zipStream != null)
            {

                string fileName = $"RelatorioAtividades - {Guid.NewGuid()}.zip";
                await _blobStorageRepository.SavePdfAsync(zipStream, fileName);

                obj.Dados = fileName;
            }

            obj.DataAlteracao = dataAlteracao;
            await _exportacaoPlanilhaRepository.UpdateAsync(obj);
        }

        public async Task<Stream> GetByIdAsync(int id)
        {
            var arquivoZip = await _exportacaoPlanilhaRepository.GetByIdAsync(id);
            return await _blobStorageRepository.GetPdfAsync(arquivoZip.Dados);
        }
    }
}
