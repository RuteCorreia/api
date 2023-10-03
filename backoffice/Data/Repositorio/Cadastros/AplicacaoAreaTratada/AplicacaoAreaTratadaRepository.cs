using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoAreaTratada
{
    public class AplicacaoAreaTratadaRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>, IAplicacaoAreaTratadaRepository
    {
        public AplicacaoAreaTratadaRepository(DataContext context) : base(context)
        {
        }
    }
}
