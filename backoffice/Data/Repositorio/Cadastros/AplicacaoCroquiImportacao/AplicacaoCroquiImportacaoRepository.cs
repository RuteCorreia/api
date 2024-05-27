using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCroquiImportacao
{
    public class AplicacaoCroquiImportacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>, IAplicacaoCroquiImportacaoRepository
    {
        protected readonly DataContext _context;

        public AplicacaoCroquiImportacaoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoCroquiImportacao.Where(x => x.Id == Id).Include("AplicacaoCroqui")
                                                         .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> ListarTodasAplicacoesCroquiImportacoes()
        {
            var obj = _context.AplicacaoCroquiImportacao.Include("AplicacaoCroqui")
                                                        .ToList();
            return obj;
        }
    }
}
