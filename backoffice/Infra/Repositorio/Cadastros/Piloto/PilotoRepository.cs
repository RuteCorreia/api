using Dapper;
using Domain.Entidades.Cadastros.Piloto;
using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Piloto;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Piloto;

public class PilotoRepository : IPilotoRepository
{
    private readonly ContextBase _contextBase;

    public PilotoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }
    
    public async Task<IEnumerable<Domain.Entidades.Cadastros.Piloto.Piloto>> GetAllAsync(int idEmpresa)
    {
        var query = @"SELECT u.Id as IdPiloto, u.Nome, u.Email, u.Telefone, u.Assinatura, uc.Credencial as CDAC FROM Usuario u
                    JOIN UsuarioCredencial uc ON u.Id = uc.IdUsuario
                    JOIN AspNetUserRoles r ON u.UserId = r.UserId
                    WHERE u.IdEmpresa = @IdEmpresa
                    AND (r.RoleId = '4ac92ff7-0d7f-4bde-9cd1-0532a2a5e372' OR r.RoleId = '63e712a0-c4c1-4295-8e91-64f0e43eb7fb')";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Piloto.Piloto>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa)
    {
        var obj = await _contextBase.UsuarioCredencial
           .Where(x =>
               x.Funcao == ERole.PilotoAeronave
               && x.Usuario != null
               && !x.Usuario.Removido
               && x.IdUsuario == Guid.Parse(id)
               && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa)
            )
           .Include(u => u.Usuario)
           .FirstOrDefaultAsync();
        return obj;
    }
}
