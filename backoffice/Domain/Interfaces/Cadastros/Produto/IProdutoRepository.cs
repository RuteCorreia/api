using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Produto
{
    public interface IProdutoRepository : IBaseRepository<Entities.Entidades.Cadastros.Produtos.Produto>
    {
        Entities.Entidades.Cadastros.Produtos.Produto BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Produtos.Produto> ListarProdutos();
    }
}
