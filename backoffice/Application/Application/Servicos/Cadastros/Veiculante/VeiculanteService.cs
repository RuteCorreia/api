using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Veiculante;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Veiculante
{
    public class VeiculanteService : BaseService<Domain.Entidades.Cadastros.Veiculante.Veiculante>, IVeiculanteService
    {
        public VeiculanteService(IBaseRepository<Domain.Entidades.Cadastros.Veiculante.Veiculante> baseRepository) : base(baseRepository)
        {
        }
    }
}
