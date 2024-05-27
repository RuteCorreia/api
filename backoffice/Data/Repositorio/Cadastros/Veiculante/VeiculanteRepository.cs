using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Veiculante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Veiculante
{
    public class VeiculanteRepository : BaseRepository<Entities.Entidades.Cadastros.Veiculante.Veiculante>, IVeiculanteRepository
    {
        public VeiculanteRepository(DataContext context) : base(context)
        {
        }
    }
}
