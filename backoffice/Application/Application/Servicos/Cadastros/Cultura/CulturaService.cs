using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Cultura
{
    public class CulturaService : BaseService<Domain.Entidades.Cadastros.Cultura.Cultura>, ICulturaService
    {
        public CulturaService(IBaseRepository<Domain.Entidades.Cadastros.Cultura.Cultura> baseRepository) : base(baseRepository)
        {
        }
    }
}
