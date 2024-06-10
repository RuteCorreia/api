using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Componentes;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.Componentes
{
    public class ComponenteRepository : IComponenteRepository
    {
        private readonly ContextBase _contextBase;

        public ComponenteRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.Componentes.Componentes obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Componentes.Componentes>> GetAllAsync(int idEmpresa)
        {            
            var entities = await _contextBase.Componente
                .AsNoTracking()
                .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
                .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Componentes.Componentes> GetByIdAsync(int id)
        {
            var obj = await _contextBase.Componente.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Componentes.Componentes obj)
        {
            var objeto = await _contextBase.Componente.FindAsync(obj.Id);
            objeto.NomeComponente = obj.NomeComponente;
            objeto.IdAeronave = obj.IdAeronave;
            objeto.Grupo = obj.Grupo;
            objeto.PartNumber = obj.PartNumber;
            objeto.SerialNumber = obj.SerialNumber;
            objeto.TBO = obj.TBO;
            objeto.UltimaInspecao = obj.UltimaInspecao;
            objeto.PrazoParaInspecao = obj.PrazoParaInspecao;
            objeto.TSN = obj.TSN;
            objeto.TSO = obj.TSO;
            objeto.EnumTBO = obj.EnumTBO;
            objeto.EnumTLV = obj.EnumTLV;

            _contextBase.Componente.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Componentes.Componentes>> GetByIdAeronaveAsync(int id)
        {
            var componentesDaAeronave = await _contextBase.Componente
            .AsNoTracking()
            .Where(x => x.IdAeronave == id)
                .ToListAsync();
            return componentesDaAeronave;
        }
    }
}
