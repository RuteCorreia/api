using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public class AplicacaoRecomendacoesTecnicasService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>, IAplicacaoRecomendacoesTecnicasService
    {
        public AplicacaoRecomendacoesTecnicasService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> baseRepository) : base(baseRepository)
        {
        }
    }
}
