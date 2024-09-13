using Domain.Entidades.Export_Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Export_Excel
{
    public interface IExportacaoPlanilhaRepository
    {
        Task<int> AddAsync(PlanilhaExcelExportada planilhaExcel);
        Task<PlanilhaExcelExportada> GetByIdAsync(int id);
        Task UpdateAsync(PlanilhaExcelExportada obj);
        Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync(int idEmpresa);
        Task<PlanilhaExcelExportada> GetFileByNameAsync(int idEmpresa, string name);
    }
}
