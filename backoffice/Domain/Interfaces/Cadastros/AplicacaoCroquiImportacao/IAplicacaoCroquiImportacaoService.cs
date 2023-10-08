using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao
{
    public interface IAplicacaoCroquiImportacaoService : IBaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> ListarTodasAplicacoesCroquiImportacoes();
    }
}
