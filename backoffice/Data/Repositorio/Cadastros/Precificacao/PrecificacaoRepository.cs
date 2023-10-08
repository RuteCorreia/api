using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Precificacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Precificacao
{
    public class PrecificacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Precificacao.Precificacao>, IPrecificacaoRepository
    {
        protected readonly DataContext _context;

        public PrecificacaoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Precificacao.Precificacao BuscarPorId(int? Id)
        {
            var obj = _context.Precificacao.Where(x => x.Id == Id).Include("Empresa").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Precificacao.Precificacao> ListarPrecificacoes()
        {
            var obj = _context.Precificacao.Include("Empresa").ToList();
            return obj;
        }
    }
}
