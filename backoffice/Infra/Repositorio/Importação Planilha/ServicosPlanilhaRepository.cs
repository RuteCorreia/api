using Domain.Interfaces.Importação_Planilha;
using Infra.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Importação_Planilha
{
    public class ServicosPlanilhaRepository<T> : IServicosPlanilhaRepository<T> where T : class
    {
        private readonly ContextBase _contextBase;

        public ServicosPlanilhaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        /// <summary>
        /// Conta a quantidade de registros de uma entidade específica no banco de dados, com base em um predicado.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade para contagem.</typeparam>
        /// <param name="db">O contexto do banco de dados.</param>
        /// <param name="predicate">O predicado para filtrar os registros.</param>
        /// <returns>A quantidade de registros no banco de dados.</returns>
        public int ContarRegistros<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _contextBase.Set<T>().Count(predicate);

        }
    }
}
