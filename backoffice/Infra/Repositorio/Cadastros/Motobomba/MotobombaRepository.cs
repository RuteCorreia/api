using Domain.Interfaces.Cadastros.Motobomba;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Motobomba
{
    public class MotobombaRepository : IMotobombaRepository
    {
        private readonly ContextBase _contextBase;
        public MotobombaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Motobomba.Motobomba>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
        {
            var entities = await _contextBase.Motobombas
                .Where(ab => ab.IdEmpresa == idEmpresa && ab.DataSituacao > dataUltimaSincronizacao)
                .ToListAsync();
            return entities;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.Motobomba.Motobomba obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Motobomba.Motobomba>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.Motobombas
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Motobomba.Motobomba> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.Motobombas.FindAsync(id);
            return obj;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Motobomba.Motobomba>> GetByNameAsync(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
            }
            var pistas = await _contextBase.Motobombas
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();

            return pistas;
        }

        public async Task UpdateUltimaTrocaOleoAsync(int? id, DateTime? dataUltimaTroca)
        {
            if (id == null)
            {
                throw new ArgumentException("O ID não pode ser nulo.");
            }

            var objeto = await _contextBase.Motobombas.FindAsync(id);
            if (objeto == null)
            {
                throw new KeyNotFoundException("A motobomba com o ID fornecido não foi encontrada.");
            }

            if (dataUltimaTroca.HasValue)
            {
                objeto.DataUltimaTrocaOleo = dataUltimaTroca.Value;
            }
            _contextBase.Motobombas.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Motobomba.Motobomba obj)
        {
            var objeto = await _contextBase.Motobombas.FindAsync(obj.Id);
            objeto.Nome = obj.Nome;
            objeto.DataUltimaTrocaOleo = obj.DataUltimaTrocaOleo;
            objeto.Checklist = obj.Checklist;

            _contextBase.Motobombas.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
