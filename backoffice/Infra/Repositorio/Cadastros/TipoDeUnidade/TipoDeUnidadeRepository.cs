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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade>> GetByDateAsync(DateTime dataUltimaSincronizacao)
        {
            var entities = await _contextBase.TipoDeUnidade
                .Where(e => e.DataSituacao > dataUltimaSincronizacao)
                .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.TipoDeUnidade.FindAsync(id);
            return obj;
        }
    }
}
