namespace Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;

public interface ICaracteristicasProdutoAplicadoRepository
{
    Task<int> AddAsync(Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj);
    Task UpdateAsync(Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<string> GetReceituarioAgronomicoAsync(int? id);
    Task AdicionarReceituarioAgronomicoAsync(int id, string data);
    Task<IEnumerable<Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>> GetAllAsync(int idEmpresa);
    Task<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetForExportExcelAsync(int? id);
    Task<Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetByIdAsync(int id, int idEmpresa);
}
