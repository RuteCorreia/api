using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoCroqui
{
    public class AplicacaoCroquiService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>, IAplicacaoCroquiService
    {
        public AplicacaoCroquiService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> baseRepository) : base(baseRepository)
        {
        }
    }
}
