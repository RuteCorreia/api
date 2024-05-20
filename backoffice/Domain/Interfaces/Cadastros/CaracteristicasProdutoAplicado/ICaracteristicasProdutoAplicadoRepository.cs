namespace Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;

public interface ICaracteristicasProdutoAplicadoRepository
{
    Task AddAsync(Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj);
    Task UpdateAsync(Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetByIdAsync(int id, int idEmpresa);
}
