using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlturaVoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AlturaVoo
{
    public class AlturaVooRepository : BaseRepository<Entities.Entidades.Cadastros.Altura_Voo.AlturaVoo>, IAlturaVooRepository
    {
        public AlturaVooRepository(DataContext context) : base(context)
        {
        }
    }
}
