using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AlvoBiologico
{
    public class AlvoBiologicoRepository : BaseRepository<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>, IAlvoBiologicoRepository
    {
        public AlvoBiologicoRepository(DataContext context) : base(context)
        {
        }
    }
}
