using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoCroquiImportacao
{
    public class AplicacaoCroquiImportacaoService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>, IAplicacaoCroquiImportacaoService
    {
        public AplicacaoCroquiImportacaoService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> baseRepository) : base(baseRepository)
        {
        }
    }
}
