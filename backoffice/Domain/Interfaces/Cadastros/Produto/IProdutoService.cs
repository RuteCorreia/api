using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Produto
{
    public interface IProdutoService : IBaseService<Entidades.Cadastros.Produto.Produto>
    {
        Entidades.Cadastros.Produto.Produto BuscarPorId(int? Id);
        List<Entidades.Cadastros.Produto.Produto> ListarProdutos();
    }
}
