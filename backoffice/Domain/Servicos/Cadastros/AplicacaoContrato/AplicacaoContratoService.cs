using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoContrato
{
    public class AplicacaoContratoService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato>, IAplicacaoContratoService
    {
        public AplicacaoContratoService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato> baseRepository) : base(baseRepository)
        {
        }
    }
}
