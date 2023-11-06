using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Produto;

namespace Application.Application.Servicos.Cadastros.Produto
{
    public class ProdutoService : BaseService<Domain.Entidades.Cadastros.Produto.Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository) : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Domain.Entidades.Cadastros.Produto.Produto BuscarPorId(int? Id)
        {
            var obj = _produtoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Produto.Produto> ListarProdutos()
        {
            var obj = _produtoRepository.ListarProdutos();
            return obj;
        }
    }
}
