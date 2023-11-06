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
    public class AplicacaoCaracteristicasService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>, IAplicacaoCaracteristicasService
    {
        private readonly IAplicacaoCaracteristicasRepository _aplicacaoCaracteristicasRepository;

        public AplicacaoCaracteristicasService(IAplicacaoCaracteristicasRepository aplicacaoCaracteristicasRepository) : base(aplicacaoCaracteristicasRepository)
        {
            _aplicacaoCaracteristicasRepository = aplicacaoCaracteristicasRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas BuscarPorId(int? Id)
        {
            var obj = _aplicacaoCaracteristicasRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> ListarTodasAplicacoesCaracteristicas()
        {
            var obj = _aplicacaoCaracteristicasRepository.ListarTodasAplicacoesCaracteristicas();
            return obj;
        }
    }
}
