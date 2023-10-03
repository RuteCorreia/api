using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoCaracteristicas
{
    public class AplicacaoCaracteristicasService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>, IAplicacaoCaracteristicasService
    {
        public AplicacaoCaracteristicasService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> baseRepository) : base(baseRepository)
        {
        }
    }
}
