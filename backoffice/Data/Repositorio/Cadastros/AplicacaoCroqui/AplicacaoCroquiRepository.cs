using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCroqui
{
    public class AplicacaoCroquiRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>, IAplicacaoCroquiRepository
    {
        protected readonly DataContext _context;

        public AplicacaoCroquiRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoCroqui.Where(x => x.Id == Id).Include("Aplicacao")
                                                                     .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> ListarTodasAplicacoesCroqui()
        {
            var obj = _context.AplicacaoCroqui.Include("Aplicacao").ToList();

            return obj;
        }
    }
}
