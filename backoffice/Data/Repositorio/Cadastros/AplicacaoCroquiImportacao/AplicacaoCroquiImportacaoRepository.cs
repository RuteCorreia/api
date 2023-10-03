using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCroquiImportacao
{
    public class AplicacaoCroquiImportacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>, IAplicacaoCroquiImportacaoRepository
    {
        public AplicacaoCroquiImportacaoRepository(DataContext context) : base(context)
        {
        }
    }
}
