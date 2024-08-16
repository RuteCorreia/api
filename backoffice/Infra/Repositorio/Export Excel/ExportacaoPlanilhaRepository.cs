using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Produto;
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

        public async Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync(int idEmpresa)
        {
            var query = "SELECT * FROM PlanilhaExcelExportadas WHERE IdEmpresa = @IdEmpresa";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var parameters = new { IdEmpresa = idEmpresa };
                var result = await connection.QueryAsync<PlanilhaExcelExportada>(query, parameters);
                return result.ToList();
            }
        }
    }
}
