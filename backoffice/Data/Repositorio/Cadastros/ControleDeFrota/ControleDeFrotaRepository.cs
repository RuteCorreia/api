using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.ControleDeFrota
{
    public class ControleDeFrotaRepository : BaseRepository<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>, IControleDeFrotaRepository
    {
        public ControleDeFrotaRepository(DataContext context) : base(context)
        {
        }
    }
}
