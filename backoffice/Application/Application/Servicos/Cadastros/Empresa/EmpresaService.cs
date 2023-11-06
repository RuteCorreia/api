using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Empresa
{
    public class EmpresaService : BaseService<Domain.Entidades.Cadastros.Empresa.Empresa>, IEmpresaService
    {
        public EmpresaService(IBaseRepository<Domain.Entidades.Cadastros.Empresa.Empresa> baseRepository) : base(baseRepository)
        {
        }
    }
}
