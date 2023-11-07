using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;

namespace Application.Application.Servicos.Cadastros.AplicacaoAreaTratada
{
    public class AplicacaoAreaTratadaService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>, IAplicacaoAreaTratadaService
    {
        private readonly IAplicacaoAreaTratadaRepository _aplicacaoAreaTratadaRepository;

        public AplicacaoAreaTratadaService(IAplicacaoAreaTratadaRepository aplicacaoAreaTratadaRepository) : base(aplicacaoAreaTratadaRepository)
        {
            _aplicacaoAreaTratadaRepository = aplicacaoAreaTratadaRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada BuscarPorId(int? Id)
        {
            var obj = _aplicacaoAreaTratadaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> ListarTodasAplicacoesAreaTratadas()
        {
            var obj = _aplicacaoAreaTratadaRepository.ListarTodasAplicacoesAreaTratadas();
            return obj;
        }
    }
}
