using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoRelatorio
{
    public interface IAplicacaoRelatorioRepository : IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> ListarTodasAplicacoesRelatorio();
    }
}
