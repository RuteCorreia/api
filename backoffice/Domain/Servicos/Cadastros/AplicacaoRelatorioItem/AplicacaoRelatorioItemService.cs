using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoRelatorioItem
{
    public class AplicacaoRelatorioItemService : BaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>, IAplicacaoRelatorioItemService
    {
        private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;

        public AplicacaoRelatorioItemService(IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository) : base(aplicacaoRelatorioItemRepository)
        {
            _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
        }

        public Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem BuscarPorId(int? Id)
        {
            var obj = _aplicacaoRelatorioItemRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> ListarTodasAplicacoesRelatorioItem()
        {
            var obj = _aplicacaoRelatorioItemRepository.ListarTodasAplicacoesRelatorioItem();
            return obj;
        }
    }
}
