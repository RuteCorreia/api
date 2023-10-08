using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoCroqui
{
    public class AplicacaoCroquiService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>, IAplicacaoCroquiService
    {
        private readonly IAplicacaoCroquiRepository _aplicacaoCroquiRepository;

        public AplicacaoCroquiService(IAplicacaoCroquiRepository aplicacaoCroquiRepository) : base(aplicacaoCroquiRepository)
        {
            _aplicacaoCroquiRepository = aplicacaoCroquiRepository;
        }

        public Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui BuscarPorId(int? Id)
        {
            var obj = _aplicacaoCroquiRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> ListarTodasAplicacoesCroqui()
        {
            var obj = _aplicacaoCroquiRepository.ListarTodasAplicacoesCroqui();
            return obj;
        }
    }
}
