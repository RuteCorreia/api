using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Executor;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Executor;

public class ExecutorRepository : IExecutorRepository
{
    private readonly ContextBase _contextBase;

    public ExecutorRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Usuario obj)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //await _contextBase.AddAsync(obj);
        //await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //var entityToRemove = await GetByIdAsync(id);
        //if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        //{
        //    _contextBase.Remove(entityToRemove);
        //    await _contextBase.SaveChangesAsync();
        //}
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => x.Usuario != null && !x.Usuario.Removido && x.Funcao == ERole.TecnicoExecutor)
            .Select(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<Usuario> GetByIdAsync(string id)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => x.Funcao == ERole.TecnicoExecutor && x.IdUsuario == Guid.Parse(id))
            .Select(u => u.Usuario)
            .FirstOrDefaultAsync();

        return obj;
    }

    public async Task<Usuario> GetByLoginAsync(string email, string password)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //var obj = await _contextBase.Executor.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
        //return obj;
        return null;
    }

    public async Task UpdateAsync(Usuario obj)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //var objeto = await _contextBase.Executor.FindAsync(obj.IdExecutor);
        //objeto.IdEmpresa = obj.IdEmpresa;
        //objeto.Nome = obj.Nome;
        //objeto.Email = obj.Email;
        //objeto.Senha = obj.Senha;
        //objeto.CFTA = obj.CFTA;
        //objeto.Assinatura = obj.Assinatura;

        //_contextBase.Executor.Update(objeto);
        //await _contextBase.SaveChangesAsync();
    }
}
