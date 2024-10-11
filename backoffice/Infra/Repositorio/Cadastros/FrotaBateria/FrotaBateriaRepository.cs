using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.FrotaBateria;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.FrotaBateria
{
    public class FrotaBateriaRepository : IFrotaBateriaRepository
    {
        private readonly ContextBase _contextBase;
        public FrotaBateriaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
            return obj.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await _contextBase.FrotaBaterias
                           .FirstOrDefaultAsync(fg => fg.Id == id);

            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.FrotaBaterias
                                    .Where(ab => ab.IdEmpresa == idEmpresa)
                                    .ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria>> GetByIdAsync(int? id)
        {
            var entities = await _contextBase.FrotaBaterias
                                    .Where(fb => fb.Id == id)
                                    .ToListAsync();
            return entities;
        }

        public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria obj)
        {
            var objeto = await _contextBase.FrotaBaterias.FindAsync(obj.Id);
            objeto.IdBateria = obj.IdBateria;
            objeto.IdFrota = obj.IdFrota;
            objeto.CicloInicial = obj.CicloInicial;
            objeto.CicloFinal = obj.CicloFinal;

            _contextBase.FrotaBaterias.Update(objeto);
            await _contextBase.SaveChangesAsync();
            return objeto.Id;
        }
    }
}
