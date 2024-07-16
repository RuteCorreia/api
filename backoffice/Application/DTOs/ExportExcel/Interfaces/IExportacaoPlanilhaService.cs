using Domain.Entidades.Export_Excel;

namespace Application.DTOs.ExportExcel.Interfaces
{
    public interface IExportacaoPlanilhaService
    {
        Task<int> AddAsync(MemoryStream zipStream, string nomeArquivo);
        Task<MemoryStream> GetByIdAsync(int id);
    }
}
