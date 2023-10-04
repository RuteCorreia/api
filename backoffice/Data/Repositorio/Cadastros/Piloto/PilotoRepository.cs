using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Piloto
{
    public class PilotoRepository : BaseRepository<Entities.Entidades.Cadastros.Pilotos.Piloto>, IPilotoRepository
    {
        protected readonly DataContext _context;

        public PilotoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id)
        {
            var obj = _context.Piloto.Where(x => x.IdPiloto == Id).Include("Empresa").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos()
        {
            var obj = _context.Piloto.Include("Empresa").ToList();
            return obj;
        }
    }
}
