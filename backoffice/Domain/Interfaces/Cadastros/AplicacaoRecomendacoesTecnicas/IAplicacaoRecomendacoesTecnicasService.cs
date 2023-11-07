using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public interface IAplicacaoRecomendacoesTecnicasService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>
    {
        Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> ListarTodasAplicacoesRecomendacoesTecnicas();
    }
}
