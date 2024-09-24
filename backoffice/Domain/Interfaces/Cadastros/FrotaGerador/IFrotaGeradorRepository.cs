namespace Domain.Interfaces.Cadastros.FrotaGerador
{
    public interface IFrotaGeradorRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.FrotaGerador.FrotaGerador obj);
        Task<int> UpdateAsync(Entidades.Cadastros.FrotaGerador.FrotaGerador obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.FrotaGerador.FrotaGerador>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.FrotaGerador.FrotaGerador> GetByIdAsync(int? id);
    }
}
