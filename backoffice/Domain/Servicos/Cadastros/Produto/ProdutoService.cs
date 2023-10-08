using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Precificacao;
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
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository) : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Entities.Entidades.Cadastros.Produtos.Produto BuscarPorId(int? Id)
        {
            var obj = _produtoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Produtos.Produto> ListarProdutos()
        {
            var obj = _produtoRepository.ListarProdutos();
            return obj;
        }
    }
}
