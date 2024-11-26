using Domain.Interfaces.Cadastros.TipoDeFormulacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.TipoDeFormulacao
{
    public class TipoDeFormulacaoRepository : ITipoDeFormulacaoRepository
    {
        private readonly ContextBase _contextBase;

        public TipoDeFormulacaoRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.TipoDeFormulacao.
                                        Where(ab => ab.IdEmpresa == 196 || ab.IdEmpresa == idEmpresa)
                                        .OrderBy(ab => ab.NomeFormulacao)
                                        .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByNameAsync(string name, int? idEmpresa)
        {
            var obj = _contextBase.TipoDeFormulacao.Where(x => x.NomeFormulacao == name && (x.IdEmpresa == idEmpresa && x.IdEmpresa == 196)).FirstOrDefault();
            return obj;
        }

        public async Task<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.TipoDeFormulacao.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj)
        {
            var objeto = await _contextBase.TipoDeFormulacao.FindAsync(obj.Id);
            objeto.NomeFormulacao = obj.NomeFormulacao;

            _contextBase.TipoDeFormulacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
