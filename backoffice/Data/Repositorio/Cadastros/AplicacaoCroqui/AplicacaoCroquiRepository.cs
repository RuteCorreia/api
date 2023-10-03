using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCroqui
{
    public class AplicacaoCroquiRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>, IAplicacaoCroquiRepository
    {
        public AplicacaoCroquiRepository(DataContext context) : base(context)
        {
        }
    }
}
