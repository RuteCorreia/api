using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Aeronave
{
    public class AeronaveRepository : BaseRepository<Entities.Entidades.Cadastros.Aeronaves.Aeronave>, IAeronaveRepository
    {
        public AeronaveRepository(DataContext context) : base(context)
        {
        }
    }
}
