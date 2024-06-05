namespace Domain.Interfaces.Cadastros.DadosResponsavel;

public interface IDadosResponsavelRepository
{
    Task<int> AddAsync(Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj);
    Task UpdateAsync(Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.DadosResponsavel.DadosResponsavel>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.DadosResponsavel.DadosResponsavel> GetByIdAsync(int id, int idEmpresa);
}
