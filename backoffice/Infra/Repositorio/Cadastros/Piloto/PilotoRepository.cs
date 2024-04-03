using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Piloto;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Piloto;

public class PilotoRepository : IPilotoRepository
{
    private readonly ContextBase _contextBase;

    public PilotoRepository(ContextBase contextBase)
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
            .Where(x => x.Usuario != null && !x.Usuario.Removido && x.Funcao == ERole.Piloto)
            .Select(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<Usuario> GetByIdAsync(string id)
    {
        var obj = await _contextBase.UsuarioCredencial
           .Where(x => x.Funcao == ERole.Piloto && x.IdUsuario == Guid.Parse(id))
           .Select(u => u.Usuario)
           .FirstOrDefaultAsync();

        return obj;
    }

    public async Task<Usuario> GetByLoginAsync(string email, string password)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //var obj = await _contextBase.Piloto.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
        //return obj;
        return null;
    }

    public async Task UpdateAsync(Usuario obj)
    {
        //VERIFICAR A NECESSIDADE DA EXISTENCIA DESSE METODO
        //var objeto = await _contextBase.Piloto.FindAsync(obj.Id);
        //objeto.Nome = obj.Nome;
        //objeto.Email = obj.Email;
        //objeto.Senha = obj.Senha;
        //objeto.CANAC = obj.CANAC;
        //objeto.Assinatura = obj.Assinatura;
        //objeto.PorcentagemComissao = obj.PorcentagemComissao;
        //objeto.Telefone = obj.Telefone;

        //_contextBase.Piloto.Update(objeto);
        //await _contextBase.SaveChangesAsync();
    }
}
