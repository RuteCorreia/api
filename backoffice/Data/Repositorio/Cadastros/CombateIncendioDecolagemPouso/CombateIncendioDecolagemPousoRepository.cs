using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.CombateIncendioDecolagemPouso
{
    public class CombateIncendioDecolagemPousoRepository : BaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>, ICombateIncendioDecolagemPousoRepository
    {
        protected readonly DataContext _context;

        public CombateIncendioDecolagemPousoRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso BuscarPorId(int? Id)
        {
            var obj = _context.CombateIncendioDecolagemPouso.Where(x => x.Id == Id).Include("CombateIncendio").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> ListarTodosCombatesIncendioDecolagemPouso()
        {
            var obj = _context.CombateIncendioDecolagemPouso.Include("CombateIncendio").ToList();
            return obj;
        }
    }
}
