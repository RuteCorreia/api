using Domain.Interfaces.Cadastros.Gerador;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Gerador
{
    public class GeradorRepository : IGeradorRepository
    {
        private readonly ContextBase _contextBase;
        public GeradorRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.Gerador.Gerador obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Gerador.Gerador>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.Geradores
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Gerador.Gerador> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.Geradores.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Gerador.Gerador obj)
        {
            var objeto = await _contextBase.Geradores.FindAsync(obj.Id);
            objeto.NomeGerador = obj.NomeGerador;
            objeto.QuantidadeHoras = obj.QuantidadeHoras;
            objeto.DataUltimaTrocaOleo = obj.DataUltimaTrocaOleo;
            objeto.QuantidadeHorasTroca = obj.QuantidadeHorasTroca;

            _contextBase.Geradores.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
