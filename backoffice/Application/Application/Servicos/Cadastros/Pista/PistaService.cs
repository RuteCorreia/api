using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Pista
{
    public class PistaService : BaseService<Domain.Entidades.Cadastros.Pistas.Pista>, IPistaService
    {
        public PistaService(IBaseRepository<Domain.Entidades.Cadastros.Pistas.Pista> baseRepository) : base(baseRepository)
        {
        }
    }
}
