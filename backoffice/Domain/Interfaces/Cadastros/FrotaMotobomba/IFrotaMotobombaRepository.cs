namespace Domain.Interfaces.Cadastros.FrotaMotobomba
{
    public interface IFrotaMotobombaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba obj);
        Task<int> UpdateAsync(Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba> GetByIdAsync(int? id);
    }
}
