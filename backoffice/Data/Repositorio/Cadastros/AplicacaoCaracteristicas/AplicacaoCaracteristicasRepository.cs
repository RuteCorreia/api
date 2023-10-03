using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoCaracteristicas
{
    public class AplicacaoCaracteristicasRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>, IAplicacaoCaracteristicasRepository
    {
        public AplicacaoCaracteristicasRepository(DataContext context) : base(context)
        {
        }
    }
}
