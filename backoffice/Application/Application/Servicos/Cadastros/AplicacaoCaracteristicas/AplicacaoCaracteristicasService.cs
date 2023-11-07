using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;

namespace Application.Application.Servicos.Cadastros.AplicacaoCaracteristicas
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
