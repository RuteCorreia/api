using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRelatorioItem
{
    public class AplicacaoRelatorioItemRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>, IAplicacaoRelatorioItemRepository
    {
        protected readonly DataContext _context;

        public AplicacaoRelatorioItemRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoRelatorioItem.Where(x => x.Id == Id).Include("AplicacaoRelatorio").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> ListarTodasAplicacoesRelatorioItem()
        {
            var obj = _context.AplicacaoRelatorioItem.Include("AplicacaoRelatorio").ToList();
            return obj;
        }
    }
}
