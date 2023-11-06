using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.AlturaVoo
{
    public class AlturaVooService : BaseService<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>, IAlturaVooService
    {
        public AlturaVooService(IBaseRepository<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo> baseRepository) : base(baseRepository)
        {
        }
    }
}
