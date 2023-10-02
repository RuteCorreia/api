using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Produto
{
    public class ProdutoRepository : BaseRepository<Entities.Entidades.Cadastros.Produtos.Produto>, IProdutoRepository
    {
        public ProdutoRepository(DataContext context) : base(context)
        {
        }
    }
}
