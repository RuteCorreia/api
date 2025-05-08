using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;

namespace Application.DTOs.Cadastros.ProdutoAplicado.Interface;

public interface IProdutoAplicadoService
{
    Task<int> AddAsync(ProdutoAplicadoCaracteristicasViewModel obj);
    Task UpdateAsync(ProdutoAplicadoCaracteristicasViewModel obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<ProdutoAplicadoCaracteristicasViewModel>> GetAllByIdCaracteristicaProdutoAplicadoAsync(int idCaracteristiacaProdutoAplicado);
    Task<ProdutoAplicadoCaracteristicasViewModel> GetByIdAsync(int id);
}
