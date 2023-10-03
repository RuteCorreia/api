using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public class AplicacaoRecomendacoesTecnicasRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>, IAplicacaoRecomendacoesTecnicasRepository
    {
        public AplicacaoRecomendacoesTecnicasRepository(DataContext context) : base(context)
        {
        }
    }
}
