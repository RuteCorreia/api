using Domain.Interfaces.Cadastros.FrotaMotobomba;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.FrotaMotobomba
{
    public class FrotaMotobombaRepository : IFrotaMotobombaRepository
    {
        private readonly ContextBase _contextBase;
        public FrotaMotobombaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.FrotaMotobombas
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.FrotaMotobombas.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba obj)
        {
            var objeto = await _contextBase.FrotaMotobombas.FindAsync(obj.Id);
            objeto.Identificacao = obj.Identificacao;
            objeto.LitrosOleo = obj.LitrosOleo;
            objeto.LitrosGasolina = obj.LitrosGasolina;
            objeto.CheckList = obj.CheckList;

            _contextBase.FrotaMotobombas.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
