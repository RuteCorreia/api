using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;

namespace Application.Application.Servicos.Cadastros.AplicacaoCroqui
{
    public class AplicacaoCroquiService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>, IAplicacaoCroquiService
    {
        private readonly IAplicacaoCroquiRepository _aplicacaoCroquiRepository;

        public AplicacaoCroquiService(IAplicacaoCroquiRepository aplicacaoCroquiRepository) : base(aplicacaoCroquiRepository)
        {
            _aplicacaoCroquiRepository = aplicacaoCroquiRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui BuscarPorId(int? Id)
        {
            var obj = _aplicacaoCroquiRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> ListarTodasAplicacoesCroqui()
        {
            var obj = _aplicacaoCroquiRepository.ListarTodasAplicacoesCroqui();
            return obj;
        }
    }
}
