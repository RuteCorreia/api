using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Entities.Entidades.Cadastros.Empresa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Bula
{
    public class BulaRepository : BaseRepository<Entities.Entidades.Cadastros.Empresa.Bula>, IBulaRepository
    {
        protected readonly DataContext _context;

        public BulaRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Empresa.Bula BuscarPorId(int? Id)
        {
            var obj = _context.Bula.Where(x => x.IdBula == Id).Include("Cultura").Include("AlvoBiologico").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Empresa.Bula> ListarTodasBulas()
        {
            var obj = _context.Bula.Include("Cultura").Include("AlvoBiologico").ToList();
            return obj;
        }
    }
}
