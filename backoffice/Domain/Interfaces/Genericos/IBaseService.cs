using FluentValidation;

namespace Domain.Interfaces.Genericos;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<TEntity> Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

    Task Remover(int id);

    Task<IEnumerable<TEntity>> Listar();

    Task<TEntity> BuscarPorId(int id);

    Task<TEntity> Atualizar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
}
