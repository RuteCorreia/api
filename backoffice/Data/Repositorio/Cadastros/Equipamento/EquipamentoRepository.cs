using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Equipamento
{
    public class EquipamentoRepository : BaseRepository<Entities.Entidades.Cadastros.Equipamento.Equipamento>, IEquipamentoRepository
    {
        public EquipamentoRepository(DataContext context) : base(context)
        {
        }
    }
}
