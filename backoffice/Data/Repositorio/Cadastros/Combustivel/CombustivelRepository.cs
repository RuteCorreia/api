using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.Cultura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Combustivel
{
    public class CombustivelRepository : BaseRepository<Entities.Entidades.Cadastros.Combustivel.Combustivel>, ICombustivelRepository
    {
        public CombustivelRepository(DataContext context) : base(context)
        {
        }
    }
}
