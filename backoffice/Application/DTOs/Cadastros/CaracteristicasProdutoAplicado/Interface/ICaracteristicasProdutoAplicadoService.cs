using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;

public interface ICaracteristicasProdutoAplicadoService
{
    Task<IEnumerable<CaracteristicasProdutoAplicadoViewModel>> GetAllAsync(string? idEmpresa);

    Task<CaracteristicasProdutoAplicadoViewModel> GetByIdAsync(int id, string? idEmpresa);

    Task<int> AddAsync(CaracteristicasProdutoAplicadoViewModel obj, string? idEmpresa);

    Task UpdateAsync(CaracteristicasProdutoAplicadoViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);
}
