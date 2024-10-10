using Domain.Interfaces.Cadastros.Bateria;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Bateria
{
    public class BateriaRepository : IBateriaRepository
    {
        private readonly ContextBase _contextBase;
        public BateriaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.Bateria.Bateria obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Bateria.Bateria>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.Baterias
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Bateria.Bateria> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.Baterias.FindAsync(id);
            return obj;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Bateria.Bateria>> GetByNameAsync(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
            }
            var baterias = await _contextBase.Baterias
                .Where(p => p.NomeBateria.Contains(nome))
                .ToListAsync();

            return baterias;
        }

        public async Task UpdateCicloAtualAsync(int? id, int? cicloAtual)
        {
            if (id == null)
            {
                throw new ArgumentException("O ID não pode ser nulo.");
            }

            var objeto = await _contextBase.Baterias.FindAsync(id);
            if (objeto == null)
            {
                throw new KeyNotFoundException("A bateria com o ID fornecido não foi encontrada.");
            }

            if (cicloAtual.HasValue)
            {
                objeto.CicloAtual = cicloAtual.Value;
            }

            _contextBase.Baterias.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Bateria.Bateria obj)
        {
            var objeto = await _contextBase.Baterias.FindAsync(obj.Id);
            objeto.NomeBateria = obj.NomeBateria;
            objeto.NumeroBateria = obj.NumeroBateria;
            objeto.CicloAtual = obj.CicloAtual;
            objeto.CicloMaximo = obj.CicloMaximo;

            _contextBase.Baterias.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
