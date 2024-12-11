using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Export_Excel;

namespace Application.DTOs.ExportExcel.Interfaces
{
    public interface IExportacaoPlanilhaService
    {
        Task<int> AddAsync(MemoryStream zipStream, string nomeArquivo,string? idEmpresa);
        Task<Stream> GetByIdAsync(int id);
        Task UpdateAsync(PlanilhaExcelExportada obj, MemoryStream zipStream);
        Task<PlanilhaExcelExportada> GetFileByNameAsync(string? idEmpresa, string name);
        Task<IEnumerable<PlanilhaExcelExportada>> GetAllAsync(string? idEmpresa);
    }
}
