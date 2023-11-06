using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Cidades
{
    public class CidadesService : BaseService<Domain.Entidades.Cadastros.Cidades.Cidades>, ICidadeService
    {
        public CidadesService(IBaseRepository<Domain.Entidades.Cadastros.Cidades.Cidades> baseRepository) : base(baseRepository)
        {
        }
    }
}
