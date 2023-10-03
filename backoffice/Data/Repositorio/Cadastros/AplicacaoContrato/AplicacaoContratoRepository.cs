using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.AplicacaoContrato
{
    public class AplicacaoContratoRepository : BaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato>, IAplicacaoContratoRepository
    {
        public AplicacaoContratoRepository(DataContext context) : base(context)
        {
        }
    }
}
