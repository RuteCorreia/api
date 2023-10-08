using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Pista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Pista
{
    public class PistaRepository : BaseRepository<Entities.Entidades.Cadastros.Pistas.Pista>, IPistaRepository
    {
        public PistaRepository(DataContext context) : base(context)
        {
        }
    }
}
