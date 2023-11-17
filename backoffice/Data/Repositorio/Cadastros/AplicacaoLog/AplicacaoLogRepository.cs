using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoLog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoLog
{
    public class AplicacaoLogRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog>, IAplicacaoLogRepository
    {
        protected readonly DataContext _context;

        public AplicacaoLogRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoLog.Where(x => x.Id == Id).Include("Aplicacao")
                                             .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog> ListarTodasAplicacoesLog()
        {
            var obj = _context.AplicacaoLog.Include("Aplicacao").ToList();
            return obj;
        }
    }
}
