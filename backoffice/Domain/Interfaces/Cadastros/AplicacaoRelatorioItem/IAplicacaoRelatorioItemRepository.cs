using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoRelatorioItem
{
    public interface IAplicacaoRelatorioItemRepository : IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> ListarTodasAplicacoesRelatorioItem();
    }
}
