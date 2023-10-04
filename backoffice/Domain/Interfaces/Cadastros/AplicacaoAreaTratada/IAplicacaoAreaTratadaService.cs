using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoAreaTratada
{
    public interface IAplicacaoAreaTratadaService : IBaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> ListarTodasAplicacoesAreaTratadas();
    }
}
