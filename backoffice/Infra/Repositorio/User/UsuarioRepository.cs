using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Produto;
using Domain.Entidades.User;
using Domain.Interfaces.User;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.User;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ContextBase _contextBase;

    public UsuarioRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Usuario obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<Usuario> GetUserByEmailAsync(string email)
    {
        return await _contextBase.Usuario.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = _contextBase.Usuario.Where(x => x.Id == id).FirstOrDefault();
        if (usuario != null) 
        {
            _contextBase.Remove(usuario);
            await _contextBase.SaveChangesAsync();
        } 
    }

    public async Task RemoveAsync(Guid id)
    {
        var usuario = _contextBase.Usuario.Where(x => x.Id == id).FirstOrDefault();
        if (usuario != null)
        {
            usuario.Removido = true;
            _contextBase.Update(usuario);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync(int? idEmpresa)
    {
       return await _contextBase.Usuario
            .AsNoTracking()
            .Where(x => !x.Removido && x.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetAllRemovidoAsync(int? idEmpresa)
    {
        return await _contextBase.Usuario
             .AsNoTracking()
             .Where(x => x.IdEmpresa == idEmpresa)
             .ToListAsync();
    }

    public async Task<Usuario> GetUserByEmpresaAndNameAsync(int idEmpresa)
    {
        return await _contextBase.Usuario
            .Where(u => u.IdEmpresa == idEmpresa && u.Nome.StartsWith("Adm"))
            .FirstOrDefaultAsync();
    }
    public async Task<Usuario> GetUserByIdAsync(string id) => await _contextBase.Usuario.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));


    public async Task<Usuario> GetByUserIdAsync(string id) => await _contextBase.Usuario.Include("Empresa").FirstOrDefaultAsync(x => string.Equals(x.UserId, id) && !x.Removido);

    public async Task<Usuario> GetLastAsync() => await _contextBase.Usuario
        .AsNoTracking()
        .Where(x => !x.Removido)
        .OrderBy(x => x.NrUsuario)
        .LastOrDefaultAsync();
    

    public async Task UpdateAsync(Usuario obj)
    {
        _contextBase.Update(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<Usuario> GetUserProfileAsync(string userId)
    {
        Guid id = Guid.Parse(userId);
        return await _contextBase.Usuario
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario> GetUserByNameAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
        }

        return await _contextBase.Usuario.FirstOrDefaultAsync(x => x.Nome == name);
    }

    public async Task<IEnumerable<UsuarioClienteInfo>> GetUsuariosClientesAsync(int idCliente, int? idEmpresa)
    {
        using var connection = new SqlConnection(_contextBase.ObterStringConexao());
        var sql = @"SELECT 
                u.Nome AS nome, 
                u.CPF AS documento,
                u.Telefone AS telefone,
                c.Cidade AS cidade,
                c.UF AS uf,
                a.Id AS userId,        
                u.IdCliente
            FROM dbo.AspNetUsers a
            INNER JOIN dbo.Usuario u ON u.UserId = a.Id
            INNER JOIN dbo.Cliente c ON c.IdCliente = u.IdCliente
            INNER JOIN dbo.AspNetUserRoles ur ON ur.UserId = a.Id
            INNER JOIN dbo.AspNetRoles r ON r.Id = ur.RoleId
            WHERE (@IdEmpresa IS NULL OR u.IdEmpresa = @IdEmpresa)
              AND r.Name = '12'
              AND u.IdCliente = @IdCliente;";
        var result = await connection.QueryAsync<UsuarioClienteInfo>(sql, new { IdCliente = idCliente, IdEmpresa = idEmpresa });
        return result;
    }
}
