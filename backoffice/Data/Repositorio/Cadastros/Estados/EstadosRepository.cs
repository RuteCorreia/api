using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Cadastros.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Estados
{
    public class EstadosRepository : BaseRepository<Entities.Entidades.Cadastros.Estados.Estados>, IEstadosRepository
    {
        public EstadosRepository(DataContext context) : base(context)
        {
        }
    }
}
