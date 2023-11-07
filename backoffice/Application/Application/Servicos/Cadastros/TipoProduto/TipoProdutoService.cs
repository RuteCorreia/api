using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.TipoProduto;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.TipoProduto
{
    public class TipoProdutoService : BaseService<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>, ITipoProdutoService
    {
        public TipoProdutoService(IBaseRepository<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto> baseRepository) : base(baseRepository)
        {
        }
    }
}
