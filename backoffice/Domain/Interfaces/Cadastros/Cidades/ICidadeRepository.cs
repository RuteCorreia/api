using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Cidades
{
    public interface ICidadeRepository
    {
        Task AddAsync(Entidades.Cadastros.Cidades.Cidades obj);
        Task UpdateAsync(Entidades.Cadastros.Cidades.Cidades obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Cidades.Cidades>> GetAllAsync();
        Task<Entidades.Cadastros.Cidades.Cidades> GetByIdAsync(int id);
    }
}
