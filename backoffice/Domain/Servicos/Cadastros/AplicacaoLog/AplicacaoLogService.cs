using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoLog;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoLog
{
    public class AplicacaoLogService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog>, IAplicacaoLogService
    {
        public AplicacaoLogService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog> baseRepository) : base(baseRepository)
        {
        }
    }
}
