using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Equipamento
{
    public class EquipamentoService : BaseService<Domain.Entidades.Cadastros.Equipamento.Equipamento>, IEquipamentoService
    {
        public EquipamentoService(IBaseRepository<Domain.Entidades.Cadastros.Equipamento.Equipamento> baseRepository) : base(baseRepository)
        {
        }
    }
}
