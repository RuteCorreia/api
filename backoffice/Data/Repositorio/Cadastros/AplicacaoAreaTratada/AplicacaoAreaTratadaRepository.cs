using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoAreaTratada
{
    public class AplicacaoAreaTratadaRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>, IAplicacaoAreaTratadaRepository
    {
        protected readonly DataContext _context;

        public AplicacaoAreaTratadaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoAreaTratada.Where(x => x.Id == Id).Include("Aplicacao")
                                                                          .Include("Estado")
                                                                          .Include("Cidade")
                                                                          .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> ListarTodasAplicacoesAreaTratadas()
        {
            var obj = _context.AplicacaoAreaTratada.Include("Aplicacao")
                                                   .Include("Estado")
                                                   .Include("Cidade")
                                                   .ToList();
            return obj;
        }
    }
}
