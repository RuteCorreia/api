namespace Domain.Interfaces.Cadastros.DataRelatorio
{
    public interface IDataRelatorioRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.DataRelatorio.DataRelatorio obj);
        Task<int> UpdateAsync(Entidades.Cadastros.DataRelatorio.DataRelatorio obj);
        Task DeleteAsync(int id, int idEmpresa);
        Task<IEnumerable<Entidades.Cadastros.DataRelatorio.DataRelatorio>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.DataRelatorio.DataRelatorio> GetByIdAsync(int? id, int idEmpresa);
    }
}
