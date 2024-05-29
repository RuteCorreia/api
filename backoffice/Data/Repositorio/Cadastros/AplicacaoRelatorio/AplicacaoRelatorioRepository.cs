using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRelatorio
{
    public class AplicacaoRelatorioRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>, IAplicacaoRelatorioRepository
    {
        protected readonly DataContext _context;

        public AplicacaoRelatorioRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoRelatorio.Where(x => x.Id == Id).Include("Aplicacao")
                                                                        .Include("Pista")
                                                                        .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> ListarTodasAplicacoesRelatorio()
        {
            var obj = _context.AplicacaoRelatorio.Include("Aplicacao")
                                                 .Include("Pista")
                                                 .ToList();
            return obj;
        }
    }
}
