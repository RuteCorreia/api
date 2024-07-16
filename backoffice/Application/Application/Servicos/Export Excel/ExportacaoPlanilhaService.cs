using Application.DTOs.ExportExcel.Interfaces;
using Domain.Entidades.Export_Excel;
using Domain.Interfaces.Export_Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Export_Excel
{
    public class ExportacaoPlanilhaService : IExportacaoPlanilhaService
    {
        private readonly IExportacaoPlanilhaRepository _exportacaoPlanilhaRepository;
        public ExportacaoPlanilhaService(IExportacaoPlanilhaRepository exportacaoPlanilhaRepository)
        {
            _exportacaoPlanilhaRepository = exportacaoPlanilhaRepository;
        }
        public async Task<int> AddAsync(MemoryStream zipStream, string nomeArquivo)
        {
            var arquivoZip = new PlanilhaExcelExportada
            {
                Nome = nomeArquivo,
                Dados = zipStream.ToArray()
            };

            return await _exportacaoPlanilhaRepository.AddAsync(arquivoZip);
        }

        public async Task<MemoryStream> GetByIdAsync(int id)
        {
            var arquivoZip = await _exportacaoPlanilhaRepository.GetByIdAsync(id);
            return new MemoryStream(arquivoZip.Dados);
        }
    }
}
