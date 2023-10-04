using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Piloto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Engenheiro
{
    public class EngenheiroRepository : BaseRepository<Entities.Entidades.Cadastros.Engenheiros.Engenheiro>, IEngenheiroRepository
    {
        protected readonly DataContext _context;

        public EngenheiroRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Engenheiros.Engenheiro BuscarPorId(int? Id)
        {
            var obj = _context.Engenheiro.Where(x => x.IdEngenheiro == Id).Include("Empresa")
                                                                          .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Engenheiros.Engenheiro> ListarTodosEngenheiros()
        {
            var obj = _context.Engenheiro.Include("Empresa").ToList();
            return obj;
        }
    }
}
