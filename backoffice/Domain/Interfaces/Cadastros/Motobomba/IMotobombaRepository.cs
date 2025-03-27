namespace Domain.Interfaces.Cadastros.Motobomba
{
    public interface IMotobombaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.Motobomba.Motobomba obj);
        Task UpdateAsync(Entidades.Cadastros.Motobomba.Motobomba obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Motobomba.Motobomba>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.Motobomba.Motobomba> GetByIdAsync(int? id);
        Task UpdateUltimaTrocaOleoAsync(int? id, DateTime? dataUltimaTroca);
        Task<IEnumerable<Entidades.Cadastros.Motobomba.Motobomba>> GetByNameAsync(string nome);
        Task<IEnumerable<Entidades.Cadastros.Motobomba.Motobomba>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
    }
}
