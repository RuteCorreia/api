using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Equipamento
{
    public class EquipamentoService : BaseService<Domain.Entidades.Cadastros.Equipamento.Equipamento>, IEquipamentoService
    {
        public EquipamentoService(IBaseRepository<Domain.Entidades.Cadastros.Equipamento.Equipamento> baseRepository) : base(baseRepository)
        {
        }
    }
}
