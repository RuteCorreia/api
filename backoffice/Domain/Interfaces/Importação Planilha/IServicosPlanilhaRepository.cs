using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Importação_Planilha
{
    public interface IServicosPlanilhaRepository<T> where T : class
    {
        int ContarRegistros<T>(Expression<Func<T, bool>> predicate) where T : class;
       
    }
}
