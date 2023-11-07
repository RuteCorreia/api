using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;

namespace Application.Application.Servicos.Cadastros.AplicacaoCroquiImportacao
{
    public class AplicacaoCroquiImportacaoService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>, IAplicacaoCroquiImportacaoService
    {
        private readonly IAplicacaoCroquiImportacaoRepository _aplicacaoCroquiImportacaoRepository;

        public AplicacaoCroquiImportacaoService(IAplicacaoCroquiImportacaoRepository aplicacaoCroquiImportacaoRepository) : base(aplicacaoCroquiImportacaoRepository)
        {
            _aplicacaoCroquiImportacaoRepository = aplicacaoCroquiImportacaoRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao BuscarPorId(int? Id)
        {
            var obj = _aplicacaoCroquiImportacaoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> ListarTodasAplicacoesCroquiImportacoes()
        {
            var obj = _aplicacaoCroquiImportacaoRepository.ListarTodasAplicacoesCroquiImportacoes();
            return obj;
        }
    }
}
