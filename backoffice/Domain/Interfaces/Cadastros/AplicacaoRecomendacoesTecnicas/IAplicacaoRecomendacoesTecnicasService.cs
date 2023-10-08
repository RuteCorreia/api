using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public interface IAplicacaoRecomendacoesTecnicasService : IBaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> ListarTodasAplicacoesRecomendacoesTecnicas();
    }
}
