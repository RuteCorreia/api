using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Cidades
{
    public class CidadesRepository : BaseRepository<Entities.Entidades.Cadastros.Cidades.Cidades>, ICidadeRepository
    {
        public CidadesRepository(DataContext context) : base(context)
        {
        }
    }
}
