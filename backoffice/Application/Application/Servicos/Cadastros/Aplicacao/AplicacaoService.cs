using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Aplicacao;

namespace Application.Application.Servicos.Cadastros.Aplicacao
{
    public class AplicacaoService : BaseService<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>, IAplicacaoService
    {
        private readonly IAplicacaoRepository _aplicacaoRepository;

        public AplicacaoService(IAplicacaoRepository aplicacaoRepository) : base(aplicacaoRepository)
        {
            _aplicacaoRepository = aplicacaoRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.Aplicacao BuscarPorId(int? Id)
        {
            var obj = _aplicacaoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.Aplicacao> ListarTodasAplicacoes()
        {
            var obj = _aplicacaoRepository.ListarTodasAplicacoes();
            return obj;
        }
    }
}
