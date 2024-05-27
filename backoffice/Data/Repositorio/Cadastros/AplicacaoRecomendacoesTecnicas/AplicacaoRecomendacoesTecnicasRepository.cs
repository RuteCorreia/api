using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public class AplicacaoRecomendacoesTecnicasRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>, IAplicacaoRecomendacoesTecnicasRepository
    {
        protected readonly DataContext _context;

        public AplicacaoRecomendacoesTecnicasRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas BuscarPorId(int? Id)
        {
            var obj = _context.AplicacaoRecomendacoesTecnicas.Where(x => x.Id == Id).Include("Aplicacao")
                                                                                    .Include("Veiculante")
                                                                                    .Include("Aeronave")
                                                                                    .Include("AlturaVoo")
                                                                                    .Include("TipoProduto")
                                                                                    .Include("Equipamento")
                                                                                    .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> ListarTodasAplicacoesRecomendacoesTecnicas()
        {
            var obj = _context.AplicacaoRecomendacoesTecnicas.Include("Aplicacao")
                                                             .Include("Veiculante")
                                                             .Include("Aeronave")
                                                             .Include("AlturaVoo")
                                                             .Include("TipoProduto")
                                                             .Include("Equipamento")
                                                             .ToList();
            return obj;
        }
    }
}
