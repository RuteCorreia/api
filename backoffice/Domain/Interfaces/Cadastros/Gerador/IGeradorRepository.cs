namespace Domain.Interfaces.Cadastros.Gerador
{
    public interface IGeradorRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.Gerador.Gerador obj);
        Task UpdateAsync(Entidades.Cadastros.Gerador.Gerador obj);
        Task DeleteAsync(int id, int idEmpresa);
        Task<IEnumerable<Entidades.Cadastros.Gerador.Gerador>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.Gerador.Gerador> GetByIdAsync(int? id, int idEmpresa);
        Task UpdateHorasAtualAsync(int? id, long? horasAtual, DateTime? dataUltimaTroca);
        Task<IEnumerable<Domain.Entidades.Cadastros.Gerador.Gerador>> GetByNameAsync(string nome, int idEmpresa);
        Task<IEnumerable<Entidades.Cadastros.Gerador.Gerador>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
    }
}
