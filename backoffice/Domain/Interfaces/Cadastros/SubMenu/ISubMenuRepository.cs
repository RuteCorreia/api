namespace Domain.Interfaces.Cadastros.SubMenu;

public interface ISubMenuRepository
{
    Task AddAsync(Entidades.Cadastros.SubMenu.SubMenu obj);
    Task UpdateAsync(Entidades.Cadastros.SubMenu.SubMenu obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.SubMenu.SubMenu>> GetAllAsync();
    Task<Entidades.Cadastros.SubMenu.SubMenu> GetByIdAsync(int id);
}
