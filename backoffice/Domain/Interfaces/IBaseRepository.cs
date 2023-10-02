using Entities.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Inserir(TEntity obj);

        void Atualizar(TEntity obj);

        void Remover(int id);

        IList<TEntity> ListarTodos();

        TEntity BuscarPorId(int id);
    }
}
