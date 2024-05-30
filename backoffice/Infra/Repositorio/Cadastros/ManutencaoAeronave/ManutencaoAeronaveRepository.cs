using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.ManutencaoAeronave;

public class ManutencaoAeronaveRepository : IManutencaoAeronaveRepository
{
    private readonly ContextBase _contextBase;

    public ManutencaoAeronaveRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave> AddAsync(Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.ManutencaoAeronave
            .AsNoTracking()
            .Where(x => 
                idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa 
            )
            .ToListAsync();
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
        objeto.Horimetro = obj.Horimetro;
        objeto.Documento = obj.Documento;
        objeto.FichaInspecao = obj.FichaInspecao;
        objeto.ManualAeronave = obj.ManualAeronave;
        objeto.MapaComponentes = obj.MapaComponentes;

        _contextBase.ManutencaoAeronave.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
