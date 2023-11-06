using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorio
{
    public class AplicacaoRelatorioService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>, IAplicacaoRelatorioService
    {
        private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;

        public AplicacaoRelatorioService(IAplicacaoRelatorioRepository aplicacaoRelatorioRepository) : base(aplicacaoRelatorioRepository)
        {
            _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio BuscarPorId(int? Id)
        {
            var obj = _aplicacaoRelatorioRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> ListarTodasAplicacoesRelatorio()
        {
            var obj = _aplicacaoRelatorioRepository.ListarTodasAplicacoesRelatorio();
            return obj;
        }
    }
}
