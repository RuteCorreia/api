using Domain.Interfaces.Cadastros.LocalIncendio;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.LocalIncendio
{
    public class LocalIncendioRepository : ILocalIncendioRepository
    {
        private readonly ContextBase _contextBase;

        public LocalIncendioRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.LocalIncendio.LocalIncendio obj)
        {
            _contextBase.Add(obj);
            _contextBase.SaveChanges();
            return obj.Id;
        }
    }
}
