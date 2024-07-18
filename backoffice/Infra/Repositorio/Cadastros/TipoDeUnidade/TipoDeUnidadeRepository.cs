using Domain.Interfaces.Cadastros.TipoDeUnidade;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.TipoDeUnidade
{
    public class TipoDeUnidadeRepository : ITipoDeUnidadeRepository
    {
        private readonly ContextBase _contextBase;

        public TipoDeUnidadeRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade>> GetAllAsync()
        {
            var entities = await _contextBase.TipoDeUnidade.ToListAsync();
            return entities;
        }
    }
}
