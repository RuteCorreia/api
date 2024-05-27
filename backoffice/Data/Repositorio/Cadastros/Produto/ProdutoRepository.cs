using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Produto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Produto
{
    public class ProdutoRepository : BaseRepository<Entities.Entidades.Cadastros.Produtos.Produto>, IProdutoRepository
    {
        protected readonly DataContext _context;

        public ProdutoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Produtos.Produto BuscarPorId(int? Id)
        {
            var obj = _context.Produto.Where(x => x.Id == Id).Include("Cultura").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Produtos.Produto> ListarProdutos()
        {
            var obj = _context.Produto.Include("Cultura").ToList();
            return obj;
        }
    }
}
