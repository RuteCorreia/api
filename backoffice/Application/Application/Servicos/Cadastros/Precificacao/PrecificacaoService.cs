using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Precificacao;

namespace Application.Application.Servicos.Cadastros.Precificacao
{
    public class PrecificacaoService : BaseService<Domain.Entidades.Cadastros.Precificacao.Precificacao>, IPrecificacaoService
    {
        private readonly IPrecificacaoRepository _precificacaoRepository;

        public PrecificacaoService(IPrecificacaoRepository precificacaoRepository) : base(precificacaoRepository)
        {
            _precificacaoRepository = precificacaoRepository;
        }

        public Domain.Entidades.Cadastros.Precificacao.Precificacao BuscarPorId(int? Id)
        {
            var obj = _precificacaoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Precificacao.Precificacao> ListarPrecificacoes()
        {
            var obj = _precificacaoRepository.ListarPrecificacoes();
            return obj;
        }
    }
}
