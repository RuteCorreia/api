using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoCaracteristicas
{
    public interface IAplicacaoCaracteristicasService : IBaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> ListarTodasAplicacoesCaracteristicas();
    }
}
