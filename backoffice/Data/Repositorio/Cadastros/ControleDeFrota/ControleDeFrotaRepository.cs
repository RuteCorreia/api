using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.ControleDeFrota
{
    public class ControleDeFrotaRepository : BaseRepository<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>, IControleDeFrotaRepository
    {
        protected readonly DataContext _context;

        public ControleDeFrotaRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota BuscarPorId(int? Id)
        {
            var obj = _context.ControleDeFrota.Where(x => x.Id == Id).Include("Frota")
                                                                     .Include("Aeronave")
                                                                     .Include("Piloto")
                                                                     .FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> ListarTodosControlesDeFrota()
        {
            var obj = _context.ControleDeFrota.Include("Frota")
                                              .Include("Aeronave")
                                              .Include("Piloto")
                                              .ToList();
            return obj;
        }
    }
}
