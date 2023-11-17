using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AlturaVoo;

public interface IAlturaVooRepository
{
    Task AddAsync(Entidades.Cadastros.Altura_Voo.AlturaVoo obj);
    Task UpdateAsync(Entidades.Cadastros.Altura_Voo.AlturaVoo obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Altura_Voo.AlturaVoo>> GetAllAsync();
    Task<Entidades.Cadastros.Altura_Voo.AlturaVoo> GetByIdAsync(int id);
}
