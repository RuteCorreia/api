using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.IdentificacaoAreaTratada
{
    public class IdentificacaoAreaTratadaRepository : IIdentificacaoAreaTratadaRepository
    {
        private readonly ContextBase _contextBase;

        public IdentificacaoAreaTratadaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
            return obj.Id;
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>> GetAllAsync()
        {
            var entities = await _contextBase.IdentificacaoAreaTratada.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada> GetByIdAsync(int id)
        {
            var obj = await _contextBase.IdentificacaoAreaTratada.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj)
        {
            var objeto = await _contextBase.IdentificacaoAreaTratada.FindAsync(obj.Id);
            objeto.UF = obj.UF;
            objeto.Localizacao = obj.Localizacao;
            objeto.Cultura = obj.Cultura;
            objeto.Extensao = obj.Extensao;
            objeto.Cidade = obj.Cidade;
            objeto.CroquiArea = obj.CroquiArea;

            _contextBase.IdentificacaoAreaTratada.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
