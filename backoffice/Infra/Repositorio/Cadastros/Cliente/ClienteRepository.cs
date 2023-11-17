using Domain.Interfaces.Cadastros.Cliente;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Cliente;

public class ClienteRepository : IClienteRepository
{
    private readonly ContextBase _contextBase;

    public ClienteRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Cliente.Cliente obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cliente.Cliente>> GetAllAsync()
    {
        var entities = await _contextBase.Cliente.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Cliente.Cliente> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Cliente.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Cliente.Cliente obj)
    {
        _contextBase.Cliente.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
