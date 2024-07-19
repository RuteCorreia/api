using Domain.Interfaces.Cadastros.BulaAplicacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.BulaAplicacao
{
    public class BulaAplicacaoRepository : IBulaAplicacaoRepository
    {
        private readonly ContextBase _contextBase;

        public BulaAplicacaoRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.BulaAplicacao obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.BulaAplicacao>> GetAllAsync()
        {
            var entities = await _contextBase.BulaAplicacao.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Empresa.BulaAplicacao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.BulaAplicacao.FindAsync(id);
            return obj;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.BulaAplicacao>> GetByIdBulaAsync(int id)
        {
            var obj = await _contextBase.BulaAplicacao.Where(x => x.IdBula == id).ToListAsync();
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.BulaAplicacao obj)
        {
            var objeto = await _contextBase.BulaAplicacao.FindAsync(obj.IdBulaAplicacao);
            objeto.IdCultura = obj.IdCultura;
            objeto.IdAlvoBiologico = obj.IdAlvoBiologico;
            objeto.DoseProdutoComercial = obj.DoseProdutoComercial;

            _contextBase.BulaAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}