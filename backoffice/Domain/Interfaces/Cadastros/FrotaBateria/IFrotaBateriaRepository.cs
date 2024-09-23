namespace Domain.Interfaces.Cadastros.FrotaBateria
{
    public interface IFrotaBateriaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.FrotaBateria.FrotaBateria obj);
        Task UpdateAsync(Entidades.Cadastros.FrotaBateria.FrotaBateria obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.FrotaBateria.FrotaBateria>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.FrotaBateria.FrotaBateria> GetByIdAsync(int? id);
    }
}
