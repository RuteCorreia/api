using Dapper;
using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Executor;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Executor;

public class ExecutorRepository : IExecutorRepository
{
    private readonly ContextBase _contextBase;

    public ExecutorRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Executor.Executor>> GetAllAsync(int idEmpresa)
    {
        var query = @"SELECT u.Id , u.Nome, u.Email, u.Telefone, u.Assinatura, uc.Credencial as CFTA FROM Usuario u
                    JOIN UsuarioCredencial uc ON u.Id = uc.IdUsuario
                    JOIN AspNetUserRoles r ON u.UserId = r.UserId
                    WHERE u.IdEmpresa = @IdEmpresa
                    AND r.RoleId = '4d43cec7-f717-4f60-90d0-6e8d376a7ada'";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Executor.Executor>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => 
                x.Funcao == ERole.TecnicoExecutor
                && x.Usuario != null
                && !x.Usuario.Removido
                && x.IdUsuario == Guid.Parse(id)
                && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa))
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync();

        return obj;
    }
}
