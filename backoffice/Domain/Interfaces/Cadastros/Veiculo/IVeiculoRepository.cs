namespace Domain.Interfaces.Cadastros.Veiculo;

public interface IVeiculoRepository
{
    Task AddAsync(Entidades.Cadastros.Veiculo.Veiculo obj);
    Task UpdateAsync(Entidades.Cadastros.Veiculo.Veiculo obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Veiculo.Veiculo>> GetAllAsync(int idEmpresa);
    Task UpdateKmAtualAsync(string? nomeVeiculo, int? kmAtual);
    Task<Entidades.Cadastros.Veiculo.Veiculo> GetByIdAsync(int id, int idEmpresa);
}
