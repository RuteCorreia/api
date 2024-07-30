using Dapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Bula;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
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
        var idEmpresaRodrigo = 196;
        var query = @"SELECT * FROM Bula WHERE (IdEmpresa = @IdEmpresa AND Removido = 0) OR (IdEmpresa = @IdEmpresaRodrigo AND Removido = 0)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa, IdEmpresaRodrigo = idEmpresaRodrigo };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Empresa.Bula>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Bula.FirstOrDefaultAsync(x => !x.Removido && x.IdBula == id);
        return obj;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name, int idEmpresa)
    {
        var idEmpresaAdministracao = 21;
        var obj = await _contextBase.Bula
            .FirstOrDefaultAsync(x => x.NomeProduto == name && (x.IdEmpresa == idEmpresa || x.IdEmpresa == idEmpresaAdministracao) && !x.Removido);
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
