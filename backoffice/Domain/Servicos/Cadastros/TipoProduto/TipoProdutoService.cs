using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.TipoProduto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.TipoProduto
{
    public class TipoProdutoService : BaseService<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>, ITipoProdutoService
    {
        public TipoProdutoService(IBaseRepository<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto> baseRepository) : base(baseRepository)
        {
        }
    }
}
