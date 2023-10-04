using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoContrato
{
    public class AplicacaoContratoService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato>, IAplicacaoContratoService
    {
        private readonly IAplicacaoContratoRepository _aplicacaoContratoRepository;

        public AplicacaoContratoService(IAplicacaoContratoRepository aplicacaoContratoRepository) : base(aplicacaoContratoRepository)
        {
            _aplicacaoContratoRepository = aplicacaoContratoRepository;
        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato BuscarPorId(int? Id)
        {
            var obj = _aplicacaoContratoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato> ListarTodasAplicacoesContrato()
        {
            var obj = _aplicacaoContratoRepository.ListarTodasAplicacoesContrato();
            return obj;
        }
    }
}
