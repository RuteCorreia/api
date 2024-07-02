namespace Domain.Interfaces.Cadastros.CombateIncendioPista
{
    public interface ICombateIncendioPistaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.CombateIncendio.CombateIncendioPista obj);
        Task<int> UpdateAsync(Entidades.Cadastros.CombateIncendio.CombateIncendioPista obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.CombateIncendio.CombateIncendioPista>> GetAllAsync(int? idEmpresa);
        Task<Entidades.Cadastros.CombateIncendio.CombateIncendioPista> GetByIdAsync(int id);
    }
}
