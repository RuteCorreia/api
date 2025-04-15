using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Aeronave;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Aeronave;

public class AeronaveRepository : IAeronaveRepository
{
    private readonly ContextBase _contextBase;

    public AeronaveRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aeronave.Aeronave>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
    {
        return await _contextBase.Aeronave
            .AsNoTracking()
            .Where(a =>
                !a.Removido
                && (idEmpresa == 0 ? a.IdEmpresa == null : a.IdEmpresa == idEmpresa)
                && a.DataSituacao > dataUltimaSincronizacao
            ).ToListAsync();
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aeronave.Aeronave obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            entityToRemove.Removido = true;
            _contextBase.Aeronave.Update(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aeronave.Aeronave>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.Aeronave
            .AsNoTracking()
            .Where(x => 
                !x.Removido
                && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
            )
            .ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aeronave.Aeronave> GetByIdAsync(int? id, int idEmpresa)
    {
        var obj = await _contextBase.Aeronave
            .Where(x => x.Id == id && x.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aeronave.Aeronave obj)
    {
        var objeto = await _contextBase.Aeronave.FindAsync(obj.Id);
        objeto.Fabricante = obj.Fabricante;
        objeto.Prefixo = obj.Prefixo;
        objeto.Modelo = obj.Modelo;
        objeto.SerialNumber = obj.SerialNumber;
        objeto.Tipo = obj.Tipo;
        objeto.Checklist = obj.Checklist;

        _contextBase.Aeronave.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aeronave.Aeronave>> GetByNameAsync(string name, int idEmpresa)
    {
        var obj = await _contextBase.Aeronave.Where(w => !w.Removido && w.Prefixo.Contains(name) && (idEmpresa == 0 ? w.IdEmpresa == null : w.IdEmpresa == idEmpresa)).ToListAsync();
        return obj;
    }
}
