using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Adjuvante
{
    public class AdjuvanteRepository : BaseRepository<Entities.Entidades.Cadastros.Adjuvante.Adjuvante>, IAdjuvanteRepository
    {
        public AdjuvanteRepository(DataContext context) : base(context)
        {
        }
    }
}
