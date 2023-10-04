using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AplicacaoCroqui
{
    public interface IAplicacaoCroquiRepository : IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>
    {
        Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> ListarTodasAplicacoesCroqui();
    }
}
