using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoRelatorio
{
    public class AplicacaoRelatorioService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>, IAplicacaoRelatorioService
    {
        public AplicacaoRelatorioService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> baseRepository) : base(baseRepository)
        {
        }
    }
}
