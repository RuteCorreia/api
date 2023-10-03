using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoLog
{
    public class AplicacaoLogRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog>, IAplicacaoLogRepository
    {
        public AplicacaoLogRepository(DataContext context) : base(context)
        {
        }
    }
}
