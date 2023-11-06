using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Aplicacao
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
