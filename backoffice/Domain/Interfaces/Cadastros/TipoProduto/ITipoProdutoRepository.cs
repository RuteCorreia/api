using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.TipoProduto
{
    public interface ITipoProdutoRepository : IBaseRepository<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>
    {
    }
}
