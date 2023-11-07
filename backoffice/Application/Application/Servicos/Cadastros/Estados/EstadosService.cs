using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Estados;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Estados
{
    public class EstadosService : BaseService<Domain.Entidades.Cadastros.Estados.Estados>, IEstadosService
    {
        public EstadosService(IBaseRepository<Domain.Entidades.Cadastros.Estados.Estados> baseRepository) : base(baseRepository)
        {
        }
    }
}
