using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoLog;

namespace Application.Application.Servicos.Cadastros.AplicacaoLog
{
    public class AplicacaoLogService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>, IAplicacaoLogService
    {
        private readonly IAplicacaoLogRepository _aplicacaoLogRepository;

        public AplicacaoLogService(IAplicacaoLogRepository aplicacaoLogRepository) : base(aplicacaoLogRepository)
        {
            _aplicacaoLogRepository = aplicacaoLogRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog BuscarPorId(int? Id)
        {
            var obj = _aplicacaoLogRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog> ListarTodasAplicacoesLog()
        {
            var obj = _aplicacaoLogRepository.ListarTodasAplicacoesLog();
            return obj;
        }
    }
}
