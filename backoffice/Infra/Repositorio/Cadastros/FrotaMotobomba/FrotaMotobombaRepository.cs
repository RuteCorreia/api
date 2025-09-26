using Domain.Entidades.Cadastros.Empresa;
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
            
            if (obj.DataTrocaOleo != null && obj.IdMotobomba != null)
            {
                var motobomba = await _contextBase.Motobombas.FindAsync(obj.IdMotobomba);
                motobomba.DataUltimaTrocaOleo = obj.DataTrocaOleo.Value;
                _contextBase.Motobombas.Update(motobomba);
            }

            await _contextBase.SaveChangesAsync();
            return obj.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await _contextBase.FrotaMotobombas
                           .FirstOrDefaultAsync(fg => fg.Id == id);

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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>> GetByIdAsync(int? id)
        {
            var entities = await _contextBase.FrotaMotobombas
                                    .Where(ab => ab.IdFrota == id)
                                    .Include(ab => ab.Motobomba)
                                    .ToListAsync();
            return entities;
        }

        public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba obj)
        {
            var objeto = await _contextBase.FrotaMotobombas.FindAsync(obj.Id);
            objeto.IdMotobomba = obj.IdMotobomba;
            objeto.Identificacao = obj.Identificacao;
            objeto.LitrosOleo = obj.LitrosOleo;
            objeto.LitrosGasolina = obj.LitrosGasolina;
            objeto.CheckList = obj.CheckList;
            objeto.DataTrocaOleo = obj.DataTrocaOleo;

            if (objeto.DataTrocaOleo != null && objeto.IdMotobomba != null)
            {
                var motobomba = await _contextBase.Motobombas.FindAsync(objeto.IdMotobomba);
                motobomba.DataUltimaTrocaOleo = objeto.DataTrocaOleo.Value;
                _contextBase.Motobombas.Update(motobomba);
            }

            _contextBase.FrotaMotobombas.Update(objeto);
            await _contextBase.SaveChangesAsync();
            return objeto.Id;
        }
    }
}
