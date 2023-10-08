using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Aplicacao
{
    public interface IAplicacaoRepository : IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.Aplicacao>
    {
        Entities.Entidades.Cadastros.Aplicacao.Aplicacao BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.Aplicacao> ListarTodasAplicacoes();
    }
}
