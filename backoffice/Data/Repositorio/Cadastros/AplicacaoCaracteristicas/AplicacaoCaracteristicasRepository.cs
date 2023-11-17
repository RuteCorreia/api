using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCaracteristicas
{
    public class AplicacaoCaracteristicasRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>, IAplicacaoCaracteristicasRepository
    {
        protected readonly DataContext _context;

        public AplicacaoCaracteristicasRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoCaracteristicas.Where(x => x.Id == Id).Include("Aplicacao")
                                                              .Include("Produto")
                                                              .Include("Adjuvante")
                                                              .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> ListarTodasAplicacoesCaracteristicas()
        {
            var obj = _context.AplicacaoCaracteristicas.Include("Aplicacao")
                                                  .Include("Produto")
                                                  .Include("Adjuvante")
                                                  .ToList();
            return obj;
        }
    }
}
