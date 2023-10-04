using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.CombateIncendio
{
    public class CombateIncendioRepository : BaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio>, ICombateIncendioRepository
    {
        protected readonly DataContext _context;

        public CombateIncendioRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio BuscarPorId(int? Id)
        {
            var obj = _context.CombateIncendio.Where(x => x.Id == Id).Include("Empresa")
                                                                     .Include("Executor")
                                                                     .Include("Aeronave")
                                                                     .Include("Pista")
                                                                     .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio> ListarTodosCombatesIncendio()
        {
            var obj = _context.CombateIncendio.Include("Empresa")
                                              .Include("Executor")
                                              .Include("Aeronave")
                                              .Include("Pista")
                                              .ToList();
            return obj;
        }
    }
}
