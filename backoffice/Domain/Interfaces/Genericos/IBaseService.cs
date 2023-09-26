using Entities.Entidades.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        void Remover(int id);

        IList<TEntity> Listar();

        TEntity BuscarPorId(int id);

        TEntity Atualizar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
    }
}
