using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.ManutencaoAeronave
{
    public class ManutencaoAeronaveRepository : IManutencaoAeronaveRepository
    {
        private readonly ContextBase _contextBase;

        public ManutencaoAeronaveRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>> GetAllAsync()
        {
            var entities = await _contextBase.ManutencaoAeronave.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave> GetByIdAsync(int id)
        {
            var obj = await _contextBase.ManutencaoAeronave.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj)
        {
            var objeto = await _contextBase.ManutencaoAeronave.FindAsync(obj.Id);
            objeto.IdAeronave = obj.IdAeronave;
            objeto.HorimetroInicial = obj.HorimetroInicial;
            objeto.HorasRevisao = obj.HorasRevisao;
            objeto.HorasInspecao = obj.HorasInspecao;
            objeto.Documento = obj.Documento;

            _contextBase.ManutencaoAeronave.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
