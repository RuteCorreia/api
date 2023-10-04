using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Precificacao
{
    public interface IPrecificacaoRepository : IBaseRepository<Entities.Entidades.Cadastros.Precificacao.Precificacao>
    {
        Entities.Entidades.Cadastros.Precificacao.Precificacao BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Precificacao.Precificacao> ListarPrecificacoes();
    }
}
