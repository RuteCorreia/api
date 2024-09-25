using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.FrotaGerador;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.FrotaGerador
{
    public class FrotaGeradorRepository : IFrotaGeradorRepository
    {
        private readonly ContextBase _contextBase;
        public FrotaGeradorRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.FrotaGeradores
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>> GetByIdAsync(int? id)
        {
            var entities = await _contextBase.FrotaGeradores
                                    .Where(ab => ab.Id == id)
                                    .ToListAsync();
            return entities;
        }

        public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador obj)
        {
            var objeto = await _contextBase.FrotaGeradores.FindAsync(obj.Id);
            objeto.HoraInicio = obj.HoraInicio;
            objeto.HoraFim = obj.HoraFim;
            objeto.HorasUso = obj.HorasUso;
            objeto.DataTrocaOleo = obj.DataTrocaOleo;

            _contextBase.FrotaGeradores.Update(objeto);
            await _contextBase.SaveChangesAsync();
            return objeto.Id;
        }
    }
}
