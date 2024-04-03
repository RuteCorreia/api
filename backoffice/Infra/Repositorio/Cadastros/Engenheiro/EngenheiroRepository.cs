using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Engenheiro;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Engenheiro;

public class EngenheiroRepository : IEngenheiroRepository
{
    private readonly ContextBase _contextBase;

    public EngenheiroRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Usuario obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        //var entityToRemove = await GetByIdAsync(id);
        //if (!ObjectNullValidation.IsObjectNull(entityToRemove))
        //{
        //    _contextBase.Remove(entityToRemove);
        //    await _contextBase.SaveChangesAsync();
        //}
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => x.Usuario != null && !x.Usuario.Removido && x.Funcao == ERole.EngAgronomoCoord)
            .Select(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<Usuario> GetByIdAsync(string id)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => x.Funcao == ERole.EngAgronomoCoord && x.IdUsuario == Guid.Parse(id))
            .Select(u => u.Usuario)
            .FirstOrDefaultAsync();
        return obj;
    }

    public async Task<Usuario> GetByIdEmpresaAsync(int id, int idEmpresa)
    {
        //return await _contextBase.Engenheiro.FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa && x.Id != id);
        return null;
    }

    public async Task<Usuario> GetByLoginAsync(string email, string password)
    {
        //var obj = await _contextBase.Engenheiro.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
        //return obj;
        return null;
    }

    public async Task UpdateAsync(Usuario obj)
    {
        //var objeto = await _contextBase.Engenheiro.FindAsync(obj.Id);
        //objeto.IdEmpresa = obj.IdEmpresa;
        //objeto.Nome = obj.Nome;
        //objeto.Email = obj.Email;
        //objeto.Senha = obj.Senha;
        //objeto.CREA = obj.CREA;
        //objeto.Assinatura = obj.Assinatura;
        //objeto.Telefone = obj.Telefone;
        //objeto.PorcentagemComissao = obj.PorcentagemComissao;

        //_contextBase.Engenheiro.Update(objeto);
        //await _contextBase.SaveChangesAsync();
    }
}
