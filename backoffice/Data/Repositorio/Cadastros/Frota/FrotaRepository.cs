using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Frota
{
    public class FrotaRepository : BaseRepository<Entities.Entidades.Cadastros.Frota.Frota>, IFrotaRepository
    {
        protected readonly DataContext _context;

        public FrotaRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Frota.Frota BuscarPorId(int? Id)
        {
            var obj = _context.Frota.Where(x => x.Id == Id).Include("Empresa").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Frota.Frota> ListarFrotas()
        {
            var obj = _context.Frota.Include("Empresa").ToList();
            return obj;
        }
    }
}
