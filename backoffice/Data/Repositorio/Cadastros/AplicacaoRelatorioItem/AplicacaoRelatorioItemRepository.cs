using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoRelatorioItem
{
    public class AplicacaoRelatorioItemRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>, IAplicacaoRelatorioItemRepository
    {
        public AplicacaoRelatorioItemRepository(DataContext context) : base(context)
        {
        }
    }
}
