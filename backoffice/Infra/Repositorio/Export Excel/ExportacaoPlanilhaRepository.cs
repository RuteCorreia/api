using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Empresa;
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

        public async Task UpdateAsync(PlanilhaExcelExportada obj)
        {
            var objeto = await _contextBase.PlanilhaExcelExportadas.FindAsync(obj.Id);
            objeto.Dados = obj.Dados;

            _contextBase.PlanilhaExcelExportadas.Update(objeto);
            await _contextBase.SaveChangesAsync();
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

        public async Task<PlanilhaExcelExportada> GetFileByNameAsync(int idEmpresa, string name)
        {
            var query = "SELECT * FROM PlanilhaExcelExportadas WHERE IdEmpresa = @IdEmpresa AND Nome = @Nome";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var parameters = new { IdEmpresa = idEmpresa, Nome = name };
                return await connection.QueryFirstOrDefaultAsync<PlanilhaExcelExportada>(query, parameters);
            }
        }
    }
}
