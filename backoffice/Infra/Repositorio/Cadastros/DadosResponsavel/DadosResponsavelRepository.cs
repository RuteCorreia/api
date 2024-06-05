using Domain.Interfaces.Cadastros.DadosResponsavel;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.DadosResponsavel;

public class DadosResponsavelRepository : IDadosResponsavelRepository
{
    private readonly ContextBase _contextBase;

    public DadosResponsavelRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;   
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj)
    {
        _contextBase.Add(obj);
        _contextBase.SaveChanges();
        return obj.Id;
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if (entityToRemove is not null)
        {
            _contextBase.DadosResponsavel.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.DadosResponsavel
             .AsNoTracking()
             .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
             .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel> GetByIdAsync(int id, int idEmpresa)
    {
        var obj = await _contextBase.DadosResponsavel
          .FirstOrDefaultAsync(x => x.Id == id && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj)
    {
        var objeto = await _contextBase.DadosResponsavel.FindAsync(obj.Id);
        objeto.Data = obj.Data;
        objeto.UF = obj.UF;
        objeto.Cidade = obj.Cidade;
        objeto.NomeCompleto = obj.NomeCompleto;
        objeto.Documento = obj.Documento;
        objeto.Telefone = obj.Telefone;
        objeto.assinaturaResponsavel = obj.assinaturaResponsavel;

        _contextBase.DadosResponsavel.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
