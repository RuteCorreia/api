using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoRelatorio
{
    public class AplicacaoRelatorioService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>, IAplicacaoRelatorioService
    {
        private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;

        public AplicacaoRelatorioService(IAplicacaoRelatorioRepository aplicacaoRelatorioRepository) : base(aplicacaoRelatorioRepository)
        {
            _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio BuscarPorId(int? Id)
        {
            var obj = _aplicacaoRelatorioRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> ListarTodasAplicacoesRelatorio()
        {
            var obj = _aplicacaoRelatorioRepository.ListarTodasAplicacoesRelatorio();
            return obj;
        }
    }
}
