namespace Domain.Interfaces.Genericos;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);

    Task UpdateAsync(TEntity obj);

    Task DeleteAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(int id);
}
