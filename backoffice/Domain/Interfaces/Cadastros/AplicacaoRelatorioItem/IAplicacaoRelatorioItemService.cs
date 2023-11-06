using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoRelatorioItem
{
    public interface IAplicacaoRelatorioItemService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>
    {
        Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> ListarTodasAplicacoesRelatorioItem();
    }
}
