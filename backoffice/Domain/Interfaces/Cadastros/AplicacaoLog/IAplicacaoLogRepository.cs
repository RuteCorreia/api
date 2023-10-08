using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoLog
{
    public interface IAplicacaoLogRepository : IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog> ListarTodasAplicacoesLog();
    }
}
