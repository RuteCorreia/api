using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.ExportExcel.Interfaces;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Export_Excel;
using Domain.Interfaces.Export_Excel;
using Helpers;
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
        public async Task<int> AddAsync(MemoryStream zipStream, string nomeArquivo, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var arquivoZip = new PlanilhaExcelExportada
            {
                Nome = nomeArquivo,
                Dados = zipStream.ToArray(),
                DataCriacao = DateTime.Now,
                DataAlteracao = DateTime.Now,
                IdEmpresa = idEmpresaInt
            };

            return await _exportacaoPlanilhaRepository.AddAsync(arquivoZip);
        }

        public async Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync(string? idEmpresa)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                return await _exportacaoPlanilhaRepository.GetAllAsync(idEmpresaInt);
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

        public async Task UpdateAsync(PlanilhaExcelExportada obj)
        {
            obj.DataAlteracao = DateTime.Now;
            await _exportacaoPlanilhaRepository.UpdateAsync(obj);
        }

        public async Task<MemoryStream> GetByIdAsync(int id)
        {
            var arquivoZip = await _exportacaoPlanilhaRepository.GetByIdAsync(id);
            return new MemoryStream(arquivoZip.Dados);
        }
    }
}
