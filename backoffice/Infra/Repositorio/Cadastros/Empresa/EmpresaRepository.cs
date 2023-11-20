using Domain.Interfaces.Cadastros.Empresa;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Empresa;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly ContextBase _contextBase;

    public EmpresaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.Empresa obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = GetByIdAsync(id);
        if(ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Empresa>> GetAllAsync()
    {
        var entities = await _contextBase.Empresa.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Empresa> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Empresa.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.Empresa obj)
    {
        _contextBase.Empresa.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
