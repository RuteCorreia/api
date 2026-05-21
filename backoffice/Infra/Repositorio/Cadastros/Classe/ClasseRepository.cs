using Domain.Interfaces.Cadastros.Classe;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Classe
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly ContextBase _contextBase;

        public ClasseRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Classe.Classe>> GetAllAsync()
        {
            return await _contextBase.Classe
                .OrderBy(c => c.Descricao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Classe.Classe>> GetByTipoServicoAsync(int idTipoDeServico)
        {
            return await _contextBase.Classe
                .Where(c => c.IdTipoDeServico == idTipoDeServico)
                .OrderBy(c => c.Descricao)
                .ToListAsync();
        }

        public async Task<Domain.Entidades.Cadastros.Classe.Classe?> GetByIdAsync(int id)
        {
            return await _contextBase.Classe.FindAsync(id);
        }

        public async Task<bool> ExistsDuplicateAsync(string descricao, int idTipoDeServico, int? excludeId = null)
        {
            var query = _contextBase.Classe
                .Where(c => c.Descricao != null
                    && c.Descricao.ToUpper() == descricao.ToUpper()
                    && c.IdTipoDeServico == idTipoDeServico);

            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<Domain.Entidades.Cadastros.Classe.Classe> AddAsync(Domain.Entidades.Cadastros.Classe.Classe obj)
        {
            await _contextBase.Classe.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Classe.Classe obj)
        {
            _contextBase.Classe.Update(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Entidades.Cadastros.Classe.Classe obj)
        {
            _contextBase.Classe.Remove(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task<bool> HasProdutoVinculadoAsync(int idClasse)
        {
            var classe = await _contextBase.Classe.FindAsync(idClasse);
            if (classe == null || string.IsNullOrEmpty(classe.Descricao))
                return false;

            return await _contextBase.Produto
                .AnyAsync(p => p.Classe != null && p.Classe.ToUpper() == classe.Descricao.ToUpper());
        }
    }
}
