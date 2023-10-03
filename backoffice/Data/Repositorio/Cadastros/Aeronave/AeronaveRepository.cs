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
        protected readonly DataContext _context;

        public AeronaveRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Entities.Entidades.Cadastros.Empresa.Empresa BuscarEmpresaPorId(int? Id)
        {
            var obj = _context.Empresa.Where(x => x.IdEmpresa == Id).FirstOrDefault();
            return obj;
        }
    }
}
