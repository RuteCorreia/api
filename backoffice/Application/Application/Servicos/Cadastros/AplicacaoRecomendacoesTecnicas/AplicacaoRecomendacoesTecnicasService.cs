using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;

namespace Application.Application.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas
{
    public class AplicacaoRecomendacoesTecnicasService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>, IAplicacaoRecomendacoesTecnicasService
    {
        private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;

        public AplicacaoRecomendacoesTecnicasService(IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository) : base(aplicacaoRecomendacoesTecnicasRepository)
        {
            _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;

        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas BuscarPorId(int? Id)
        {
            var obj = _aplicacaoRecomendacoesTecnicasRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> ListarTodasAplicacoesRecomendacoesTecnicas()
        {
            var obj = _aplicacaoRecomendacoesTecnicasRepository.ListarTodasAplicacoesRecomendacoesTecnicas();
            return obj;
        }
    }
}
