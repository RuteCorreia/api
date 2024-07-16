using Dapper;
using Domain.Entidades.Export_Excel;
using Domain.Interfaces.Export_Excel;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;

namespace Infra.Repositorio.Export_Excel
{
    public class ExportacaoPlanilhaRepository : IExportacaoPlanilhaRepository
    {
        private readonly ContextBase _contextBase;

        public ExportacaoPlanilhaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<int> AddAsync(PlanilhaExcelExportada planilhaExcel)
        {
            await _contextBase.PlanilhaExcelExportadas.AddAsync(planilhaExcel);
            await _contextBase.SaveChangesAsync();
            return planilhaExcel.Id; // Supondo que ArquivoZip tenha uma propriedade Id
        }

        public async Task<PlanilhaExcelExportada> GetByIdAsync(int id)
        {
            return await _contextBase.PlanilhaExcelExportadas.FindAsync(id);
        }

        public async Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync()
        {
            var query = "SELECT * FROM PlanilhaExcelExportadas";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var result = await connection.QueryAsync<PlanilhaExcelExportada>(query);
                return result.ToList();
            }
        }
    }
}
