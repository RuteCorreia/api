using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Aplicacao
{
    public class AplicacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.Aplicacao>, IAplicacaoRepository
    {
        public AplicacaoRepository(DataContext context) : base(context)
        {
        }
    }
}
