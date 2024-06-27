using Dapper;
using Domain.Interfaces.Cadastros.ContratoPrestacaoServico;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.ContratoPrestacaoServico;

public class ContratoPrestacaoServicoRepository : IContratoPrestacaoServicoRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public ContratoPrestacaoServicoRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico obj)
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
            _contextBase.ContratoPrestacaoServico.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.ContratoPrestacaoServico
             .AsNoTracking()
             .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
             .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico> GetByIdAsync(int id, int idEmpresa)
    {
        using (var connection = _dbConnection)
        {
            try
            {
                string query = $"SELECT * FROM ContratoPrestacaoServico WHERE Id = {id}";
                var produtoAplicado = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>(query);
                return produtoAplicado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico obj)
    {
        var objeto = await _contextBase.ContratoPrestacaoServico.FindAsync(obj.Id);
        objeto.DistanciaPista = obj.DistanciaPista;
        objeto.Preco = obj.Preco;
        objeto.UnidadePreco = obj.UnidadePreco;
        objeto.Extensao = obj.Extensao;
        objeto.ValorTotal = obj.ValorTotal;
        objeto.Vencimento = obj.Vencimento;
        objeto.NomePiloto = obj.NomePiloto;
        objeto.Executor = obj.Executor;

        _contextBase.ContratoPrestacaoServico.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
