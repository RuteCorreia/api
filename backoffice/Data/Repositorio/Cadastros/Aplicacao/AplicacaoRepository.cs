using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Aplicacao
{
    public class AplicacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.Aplicacao>, IAplicacaoRepository
    {
        protected readonly DataContext _context;

        public AplicacaoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Entities.Entidades.Cadastros.Aplicacao.Aplicacao BuscarPorId(int? Id)
        {
            var obj = _context.Aplicacao.Where(x => x.Id == Id).Include("Empresa").Include("Piloto")
                                                                .Include("Executor").Include("Cliente")
                                                                .Include("Cultura").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.Aplicacao> ListarTodasAplicacoes()
        {
            var obj = _context.Aplicacao.Include("Empresa").Include("Piloto")
                                                    .Include("Executor").Include("Cliente")
                                                    .Include("Cultura").ToList();
            return obj;
        }
    }
}
