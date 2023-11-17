namespace Domain.Interfaces.Generics;

public interface InterfaceGeneric<T> where T : class
{
    Task AddAsync(T Objeto);
    Task UpdateAsync(T Objeto);
    Task DeleteAsync(T Objeto);
    Task<T> GetEntityByIdAsync(int Id);
    Task<List<T>> ListAsync();
}
