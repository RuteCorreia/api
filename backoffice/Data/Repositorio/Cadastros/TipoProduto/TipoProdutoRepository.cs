using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.TipoProduto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.TipoProduto
{
    public class TipoProdutoRepository : BaseRepository<Entities.Entidades.Cadastros.Tipo_Produto.TipoProduto>, ITipoProdutoRepository
    {
        public TipoProdutoRepository(DataContext context) : base(context)
        {
        }
    }
}
