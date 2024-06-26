using Domain.Interfaces.Cadastros.RelatorioIncendio;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.RelatorioIncendio
{
    public class RelatorioIncendioRepository : IRelatorioIncendioRepository
    {
        private readonly ContextBase _contextBase;

        public RelatorioIncendioRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio obj)
        {
            _contextBase.Add(obj);
            _contextBase.SaveChanges();
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio>> GetAllAsync()
        {
            var entities = await _contextBase.RelatorioIncendio.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioIncendio.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio obj)
        {
            var objeto = await _contextBase.RelatorioIncendio.FindAsync(obj.Id);

            _contextBase.RelatorioIncendio.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
