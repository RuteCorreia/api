using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Produto
{
    public class ProdutoService : BaseService<Entities.Entidades.Cadastros.Produtos.Produto>, IProdutoService
    {
        public ProdutoService(IBaseRepository<Entities.Entidades.Cadastros.Produtos.Produto> baseRepository) : base(baseRepository)
        {
        }
    }
}
