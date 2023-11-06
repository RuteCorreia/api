using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoContrato;

namespace Application.Application.Servicos.Cadastros.AplicacaoContrato
{
    public class AplicacaoContratoService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>, IAplicacaoContratoService
    {
        private readonly IAplicacaoContratoRepository _aplicacaoContratoRepository;

        public AplicacaoContratoService(IAplicacaoContratoRepository aplicacaoContratoRepository) : base(aplicacaoContratoRepository)
        {
            _aplicacaoContratoRepository = aplicacaoContratoRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato BuscarPorId(int? Id)
        {
            var obj = _aplicacaoContratoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato> ListarTodasAplicacoesContrato()
        {
            var obj = _aplicacaoContratoRepository.ListarTodasAplicacoesContrato();
            return obj;
        }
    }
}
