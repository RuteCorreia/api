namespace Domain.Interfaces.Cadastros.RelatorioIncendio
{
    public interface IRelatorioIncendioRepository
    {
        Task AddAsync(Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio>> GetAllAsync();
        Task<Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio> GetByIdAsync(int id);
    }
}
