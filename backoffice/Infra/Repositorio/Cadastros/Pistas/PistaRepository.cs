using Domain.Entidades.Cadastros.Pistas;
using Domain.Interfaces.Cadastros.Pista;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Pistas;

public class PistaRepository : IPistaRepository
{
    private readonly ContextBase _contextBase;

    public PistaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.Pistas.Pista obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Pistas.Pista>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.Pista
        .Where(p => p.IdEmpresa == idEmpresa)
        .ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<Pista>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
    {
        var entities = await _contextBase.Pista
            .Where(ab => ab.IdEmpresa == idEmpresa && ab.DataSituacao > dataUltimaSincronizacao)
            .ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Pistas.Pista> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Pista.FindAsync(id);
        return obj;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Pistas.Pista>> GetByNameAsync(string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
        }
        var pistas = await _contextBase.Pista
            .Where(p => p.Nome.Contains(nome))
            .ToListAsync();

        return pistas;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Pistas.Pista obj)
    {
        var objeto = await _contextBase.Pista.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;
        objeto.LAT = obj.LAT;
        objeto.LONG = obj.LONG;

        _contextBase.Pista.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
