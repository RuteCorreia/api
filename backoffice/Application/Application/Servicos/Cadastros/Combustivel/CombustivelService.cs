using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Combustivel
{
    public class CombustivelService : BaseService<Domain.Entidades.Cadastros.Combustivel.Combustivel>, ICombustivelService
    {
        public CombustivelService(IBaseRepository<Domain.Entidades.Cadastros.Combustivel.Combustivel> baseRepository) : base(baseRepository)
        {
        }
    }
}
