using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.PlanoContrato
{
    public class PlanoContratoRepository : BaseRepository<PlanoDeContrato>, IPlanoContratoRepository
    {
        public PlanoContratoRepository(DataContext context) : base(context)
        {
        }
    }
}
