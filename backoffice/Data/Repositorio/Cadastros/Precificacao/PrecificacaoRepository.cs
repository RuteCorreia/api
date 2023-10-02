using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Precificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Precificacao
{
    public class PrecificacaoRepository : BaseRepository<Entities.Entidades.Cadastros.Precificacao.Precificacao>, IPrecificacaoRepository
    {
        public PrecificacaoRepository(DataContext context) : base(context)
        {
        }
    }
}
