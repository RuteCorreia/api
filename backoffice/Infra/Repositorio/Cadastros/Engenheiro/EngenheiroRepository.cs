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

    public async Task AddAsync(Domain.Entidades.Cadastros.Engenheiro.Engenheiro obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>> GetAllAsync()
    {
        var entities = await _contextBase.Engenheiro
            .AsNoTracking()
            .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Engenheiro.Engenheiro> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Engenheiro.FindAsync(id);
        return obj;
    }

    public async Task<Domain.Entidades.Cadastros.Engenheiro.Engenheiro> GetByIdEmpresaAsync(int id, int idEmpresa)
    {
        return await _contextBase.Engenheiro.FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa && x.Id != id);
    }

    public async Task<Domain.Entidades.Cadastros.Engenheiro.Engenheiro> GetByLoginAsync(string email, string password)
    {
        var obj = await _contextBase.Engenheiro.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Engenheiro.Engenheiro obj)
    {
        var objeto = await _contextBase.Engenheiro.FindAsync(obj.Id);
        objeto.IdEmpresa = obj.IdEmpresa;
        objeto.Nome = obj.Nome;
        objeto.Email = obj.Email;
        objeto.Senha = obj.Senha;
        objeto.CREA = obj.CREA;
        objeto.Assinatura = obj.Assinatura;
        objeto.Telefone = obj.Telefone;
        objeto.PorcentagemComissao = obj.PorcentagemComissao;

        _contextBase.Engenheiro.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
