using Domain.Interfaces.Cadastros.Bula;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Bula;

public class BulaRepository : IBulaRepository
{
    private readonly ContextBase _contextBase;

    public BulaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemoveOrDeactivate = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemoveOrDeactivate))
        {
            var hasFk = await _contextBase.BulaAplicacao
                .AnyAsync(x => x.IdBula == entityToRemoveOrDeactivate.IdBula);
                        
            if (hasFk)
            {
                entityToRemoveOrDeactivate.Removido = true;
                _contextBase.Bula.Update(entityToRemoveOrDeactivate);
            }
            else
            {
                _contextBase.Bula.Remove(entityToRemoveOrDeactivate);
            }

            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Bula>> GetAllAsync(int idEmpresa)
    {
        var idEmpresaRodrigo = 21;
        var entities = await _contextBase.Bula
            .AsNoTracking()
            .Where(x => !x.Removido && x.IdEmpresa == idEmpresa && x.IdEmpresa == idEmpresaRodrigo) 
            .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Bula.FirstOrDefaultAsync(x => !x.Removido && x.IdBula == id);
        return obj;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name)
    {
        var obj =  await _contextBase.Bula.FirstOrDefaultAsync(x => x.NomeProduto == name && !x.Removido);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
    {
        var objeto = await _contextBase.Bula.FindAsync(obj.IdBula);
        objeto.NomeProduto = obj.NomeProduto;
        objeto.IdCultura = obj.IdCultura;
        objeto.IdClassificacaoToxicologica = obj.IdClassificacaoToxicologica;
        objeto.Classe = obj.Classe;
        objeto.TipoDeFormulacao = obj.TipoDeFormulacao;
        objeto.IdAlvoBiologico = obj.IdAlvoBiologico;
        objeto.DoseProdutoComercial = obj.DoseProdutoComercial;
        objeto.Adjuvante = obj.Adjuvante;
        objeto.IdTipoDeServico = obj.IdTipoDeServico;
        var listaParaRemover = _contextBase.BulaAplicacao.Where(x => x.IdBula == obj.IdBula).ToList();
        _contextBase.BulaAplicacao.RemoveRange(listaParaRemover);
        _contextBase.Bula.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
