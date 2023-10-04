using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Precificacao;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Precificacao
{
    public class PrecificacaoService : BaseService<Entities.Entidades.Cadastros.Precificacao.Precificacao>, IPrecificacaoService
    {
        private readonly IPrecificacaoRepository _precificacaoRepository;

        public PrecificacaoService(IPrecificacaoRepository precificacaoRepository) : base(precificacaoRepository)
        {
            _precificacaoRepository = precificacaoRepository;
        }

        public Entities.Entidades.Cadastros.Precificacao.Precificacao BuscarPorId(int? Id)
        {
            var obj = _precificacaoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Precificacao.Precificacao> ListarPrecificacoes()
        {
            var obj = _precificacaoRepository.ListarPrecificacoes();
            return obj;
        }
    }
}
