using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoAreaTratada
{
    public class AplicacaoAreaTratadaService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>, IAplicacaoAreaTratadaService
    {
        private readonly IAplicacaoAreaTratadaRepository _aplicacaoAreaTratadaRepository;

        public AplicacaoAreaTratadaService(IAplicacaoAreaTratadaRepository aplicacaoAreaTratadaRepository) : base(aplicacaoAreaTratadaRepository)
        {
            _aplicacaoAreaTratadaRepository = aplicacaoAreaTratadaRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada BuscarPorId(int? Id)
        {
            var obj = _aplicacaoAreaTratadaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> ListarTodasAplicacoesAreaTratadas()
        {
            var obj = _aplicacaoAreaTratadaRepository.ListarTodasAplicacoesAreaTratadas();
            return obj;
        }
    }
}
