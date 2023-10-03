using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRelatorio
{
    public class AplicacaoRelatorioRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>, IAplicacaoRelatorioRepository
    {
        public AplicacaoRelatorioRepository(DataContext context) : base(context)
        {
        }
    }
}
