using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Equipamento;

public interface IEquipamentoRepository
{
    Task AddAsync(Entidades.Cadastros.Equipamento.Equipamento obj);
    Task UpdateAsync(Entidades.Cadastros.Equipamento.Equipamento obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Equipamento.Equipamento>> GetAllAsync();
    Task<Entidades.Cadastros.Equipamento.Equipamento> GetByIdAsync(int id);
}
