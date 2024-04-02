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

    //public async Task AddAsync(Domain.Entidades.Cadastros.Piloto.Piloto obj)
    //{
    //    await _contextBase.AddAsync(obj);
    //    await _contextBase.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(int id)
    //{
    //    var entityToRemove = await GetByIdAsync(id);
    //    if(!ObjectNullValidation.IsObjectNull(entityToRemove))
    //    {
    //        _contextBase.Remove(entityToRemove);
    //        await _contextBase.SaveChangesAsync();
    //    }
    //}

    //public async Task<IEnumerable<Domain.Entidades.Cadastros.Piloto.Piloto>> GetAllAsync()
    //{
    //    var entities = await _contextBase.Piloto.ToListAsync();
    //    return entities;
    //}

    //public async Task<Domain.Entidades.Cadastros.Piloto.Piloto> GetByIdAsync(int id)
    //{
    //    var obj = await _contextBase.Piloto.FindAsync(id);
    //    return obj;
    //}

    //public async Task<Domain.Entidades.Cadastros.Piloto.Piloto> GetByLoginAsync(string email, string password)
    //{
    //    var obj = await _contextBase.Piloto.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
    //    return obj;
    //}

    //public async Task UpdateAsync(Domain.Entidades.Cadastros.Piloto.Piloto obj)
    //{
    //    var objeto = await _contextBase.Piloto.FindAsync(obj.Id);
    //    objeto.Nome = obj.Nome;
    //    objeto.Email = obj.Email;
    //    objeto.Senha = obj.Senha;
    //    objeto.CANAC = obj.CANAC;
    //    objeto.Assinatura = obj.Assinatura;
    //    objeto.PorcentagemComissao = obj.PorcentagemComissao;
    //    objeto.Telefone = obj.Telefone;

    //    _contextBase.Piloto.Update(objeto);
    //    await _contextBase.SaveChangesAsync();
    //}
}
