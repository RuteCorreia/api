using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoContrato
{
    public class AplicacaoContratoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato>, IAplicacaoContratoRepository
    {
        protected readonly DataContext _context;

        public AplicacaoContratoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoContrato.Where(x => x.Id == Id).Include("Aplicacao")
                                                  .Include("Estado")
                                                  .Include("Cidade")
                                                  .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato> ListarTodasAplicacoesContrato()
        {
            var obj = _context.AplicacaoContrato.Include("Aplicacao")
                                      .Include("Estado")
                                      .Include("Cidade")
                                      .ToList();
            return obj;
        }
    }
}
