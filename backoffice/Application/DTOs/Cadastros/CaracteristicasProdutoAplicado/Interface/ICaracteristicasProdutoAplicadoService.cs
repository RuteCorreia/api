using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;

public interface ICaracteristicasProdutoAplicadoService
{
    Task<IEnumerable<CaracteristicasProdutoAplicadoViewModel>> GetAllAsync(string? idEmpresa);

    Task<ProdutoAplicadoViewModel> GetByIdAsync(int id, string? idEmpresa);

    Task<int> AddAsync(ProdutoAplicadoViewModel obj, string? idEmpresa);

    Task UpdateAsync(CaracteristicasProdutoAplicadoViewModel obj);
    Task<DataFormatViewModel> GetReceituarioAgronomicoAsync(int? id);
    Task AdicionarReceituarioAgronomicoAsync(int id, DataFormatViewModel objData);

    Task DeleteAsync(int id, string? idEmpresa);

    Task<CaracteristicasProdutoAplicadoViewModel> GetForExportExcelAsync(int id);
}
