using FluentValidation;

namespace Domain.Interfaces.Genericos;

public interface IBaseService<TEntity> where TEntity : class
{
    TEntity Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

    void Remover(int id);

    IList<TEntity> Listar();

    TEntity BuscarPorId(int id);

    TEntity Atualizar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
}
