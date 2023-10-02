using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Bula
{
    public class BulaRepository : BaseRepository<Entities.Entidades.Cadastros.Empresa.Bula>, IBulaRepository
    {
        public BulaRepository(DataContext context) : base(context)
        {
        }
    }
}
