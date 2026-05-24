using Domain.Interfaces.Cadastros.TipoDeServico;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.TipoDeServico
{
    public class TipoDeServicoRepository : ITipoDeServicoRepository
    {
        private readonly ContextBase _contextBase;

        public TipoDeServicoRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico>> GetAllAsync()
        {
            var entities = await _contextBase.TipoDeServico
                .OrderBy(t => t.NomeServico)
                .ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico>> GetByDateAsync(DateTime dataUltimaSincronizacao)
        {
            var entities = await _contextBase.TipoDeServico
                .Where(e => e.DataSituacao > dataUltimaSincronizacao)
                .OrderBy(t => t.NomeServico)
                .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico> GetByIdAsync(int id)
        {
            var obj = await _contextBase.TipoDeServico.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico obj)
        {
            var objeto = await _contextBase.TipoDeServico.FindAsync(obj.Id);
            objeto.NomeServico = obj.NomeServico;

            _contextBase.TipoDeServico.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
