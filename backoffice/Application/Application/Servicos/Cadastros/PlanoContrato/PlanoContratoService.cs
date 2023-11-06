using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Genericos;
using Domain.Entidades.Cadastros.Empresa;
using Application.Application.Servicos.Genericos;

namespace Application.Application.Servicos.Cadastros.PlanoContrato
{
    public class PlanoContratoService : BaseService<PlanoDeContrato>, IPlanoContratoService
    {
        public PlanoContratoService(IBaseRepository<PlanoDeContrato> baseRepository) : base(baseRepository)
        {
        }
    }
}
