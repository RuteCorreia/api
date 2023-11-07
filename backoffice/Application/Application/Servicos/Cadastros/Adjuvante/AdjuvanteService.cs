using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Adjuvante
{
    public class AdjuvanteService : BaseService<Domain.Entidades.Cadastros.Adjuvante.Adjuvante>, IAdjuvanteService
    {
        public AdjuvanteService(IBaseRepository<Domain.Entidades.Cadastros.Adjuvante.Adjuvante> baseRepository) : base(baseRepository)
        {
        }
    }
}
