using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AlvoBiologico
{
    public class AlvoBiologicoRepository : BaseRepository<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>, IAlvoBiologicoRepository
    {
        protected readonly DataContext _context;

        public AlvoBiologicoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico BuscarPorId(int? Id)
        {
            var obj = _context.AlvoBiologico.Where(x => x.Id == Id).Include("Produto").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> ListarTodosAlvosBiologicos()
        {
            var obj = _context.AlvoBiologico.Include("Produto").ToList();
            return obj;
        }
    }
}
