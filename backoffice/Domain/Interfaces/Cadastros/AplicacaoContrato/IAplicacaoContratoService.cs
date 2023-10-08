using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoContrato
{
    public interface IAplicacaoContratoService : IBaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato> ListarTodasAplicacoesContrato();
    }
}
