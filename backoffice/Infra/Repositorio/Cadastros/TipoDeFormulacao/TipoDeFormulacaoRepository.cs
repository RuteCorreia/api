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

        public async Task DeleteAsync(int id, int idEmpresa)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                if (entityToRemove.IdRef == null)
                    return;

                if (entityToRemove.IdEmpresa != idEmpresa)
                    return;

                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetAllAsync(int idEmpresa)
        {
            var empresaRecords = await _contextBase.TipoDeFormulacao
                .Where(t => t.IdEmpresa == idEmpresa)
                .ToListAsync();

            if (idEmpresa == 196)
                return empresaRecords.OrderBy(t => t.NomeFormulacao).ToList();

            var overriddenIds = empresaRecords
                .Where(t => t.IdRef != null)
                .Select(t => t.IdRef.Value)
                .ToHashSet();

            var defaultRecords = await _contextBase.TipoDeFormulacao
                .Where(t => t.IdEmpresa == 196 && !overriddenIds.Contains(t.Id))
                .ToListAsync();

            return empresaRecords.Concat(defaultRecords).OrderBy(t => t.NomeFormulacao).ToList();
        }

        public async Task<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByNameAsync(string name, int? idEmpresa)
        {
            var obj = _contextBase.TipoDeFormulacao.Where(x => x.NomeFormulacao == name && (x.IdEmpresa == idEmpresa || x.IdEmpresa == 196)).FirstOrDefault();
            return obj;
        }

        public async Task<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.TipoDeFormulacao.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj, int idEmpresa)
        {
            var objeto = await _contextBase.TipoDeFormulacao.FindAsync(obj.Id);
            if (objeto == null) return;

            if (objeto.IdEmpresa == idEmpresa)
            {
                objeto.NomeFormulacao = obj.NomeFormulacao;
                _contextBase.TipoDeFormulacao.Update(objeto);
                await _contextBase.SaveChangesAsync();
            }
            else
            {
                var overrideRecord = new Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao
                {
                    NomeFormulacao = obj.NomeFormulacao,
                    IdEmpresa = idEmpresa,
                    IdRef = objeto.Id
                };
                await _contextBase.AddAsync(overrideRecord);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
        {
            var empresaRecords = await _contextBase.TipoDeFormulacao
                .Where(t => t.IdEmpresa == idEmpresa && t.DataSituacao > dataUltimaSincronizacao)
                .ToListAsync();

            if (idEmpresa == 196)
                return empresaRecords.OrderBy(t => t.NomeFormulacao).ToList();

            var allOverriddenIds = await _contextBase.TipoDeFormulacao
                .Where(t => t.IdEmpresa == idEmpresa && t.IdRef != null)
                .Select(t => t.IdRef.Value)
                .ToListAsync();
            var overriddenIdsSet = allOverriddenIds.ToHashSet();

            var defaultRecords = await _contextBase.TipoDeFormulacao
                .Where(t => t.IdEmpresa == 196 && t.DataSituacao > dataUltimaSincronizacao && !overriddenIdsSet.Contains(t.Id))
                .ToListAsync();

            return empresaRecords.Concat(defaultRecords).OrderBy(t => t.NomeFormulacao).ToList();
        }
    }
}
