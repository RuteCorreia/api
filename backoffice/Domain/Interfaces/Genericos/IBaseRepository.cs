namespace Domain.Interfaces.Genericos;

public interface IBaseRepository<TEntity> where TEntity : class
{
    void Inserir(TEntity obj);

    void Atualizar(TEntity obj);

    void Remover(int id);

    IList<TEntity> ListarTodos();

    TEntity BuscarPorId(int id);
}
