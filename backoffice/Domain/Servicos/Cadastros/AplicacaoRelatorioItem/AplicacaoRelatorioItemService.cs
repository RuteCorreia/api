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
    public class AplicacaoRelatorioItemService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>, IAplicacaoRelatorioItemService
    {
        public AplicacaoRelatorioItemService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> baseRepository) : base(baseRepository)
        {
        }
    }
}
