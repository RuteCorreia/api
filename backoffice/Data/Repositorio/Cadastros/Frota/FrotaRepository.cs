using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Frota
{
    public class FrotaRepository : BaseRepository<Entities.Entidades.Cadastros.Frota.Frota>, IFrotaRepository
    {
        public FrotaRepository(DataContext context) : base(context)
        {
        }
    }
}
