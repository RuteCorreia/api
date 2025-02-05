namespace Domain.Interfaces.Cadastros.Bateria
{
    public interface IBateriaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.Bateria.Bateria obj);
        Task UpdateAsync(Entidades.Cadastros.Bateria.Bateria obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Bateria.Bateria>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.Bateria.Bateria> GetByIdAsync(int? id);
        Task UpdateCicloAtualAsync(int? id, int? cicloAtual);
        Task<IEnumerable<Entidades.Cadastros.Bateria.Bateria>> GetByNameAsync(string nome);
        Task<IEnumerable<Entidades.Cadastros.Bateria.Bateria>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
    }
}
