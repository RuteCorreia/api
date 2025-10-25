using Domain.Interfaces.Cadastros.Cliente;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Infra.Repositorio.Cadastros.Cliente;

public class ClienteRepository : IClienteRepository
{
    private readonly ContextBase _contextBase;

    public ClienteRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cliente.Cliente>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
    {
        var entities = await _contextBase.Cliente
            .AsNoTracking()
            .Where(x => (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa) && x.DataSituacao > dataUltimaSincronizacao)
            .ToListAsync();

        return entities;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Cliente.Cliente obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cliente.Cliente>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.Cliente
            .AsNoTracking()
            .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
            .ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Cliente.Cliente> GetByIdAsync(int id, int empresa)
    {
        var obj = await _contextBase.Cliente.FirstOrDefaultAsync(x => x.IdCliente == id);
        return obj;
    }
    
    public async Task<Domain.Entidades.Cadastros.Cliente.Cliente> GetByLoginAsync(string email, string password)
    {
        var obj = await _contextBase.Cliente.FirstOrDefaultAsync(w => w.Email == email && w.Senha == password);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Cliente.Cliente obj)
    {
        var objeto = await _contextBase.Cliente.FindAsync(obj.IdCliente);
        objeto.NomeCliente = obj.NomeCliente;
        objeto.IdTipoCliente = obj.IdTipoCliente;
        objeto.CPF = obj.CPF;
        objeto.RG = obj.RG;
        objeto.CNPJ = obj.CNPJ;
        objeto.InscricaoEstadual = obj.InscricaoEstadual;
        objeto.Endereco = obj.Endereco;
        objeto.Telefone1 = obj.Telefone1;
        objeto.Telefone2 = obj.Telefone2;
        objeto.Email = obj.Email;
        objeto.Precificacao = obj.Precificacao;
        objeto.Cidade = obj.Cidade;
        objeto.UF = obj.UF;

        _contextBase.Cliente.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cliente.Cliente>> GetByNameAsync(string name, int idEmpresa)
    {
        var obj = await _contextBase.Cliente.Where(x => x.NomeCliente.Contains(name) && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)).ToListAsync();
        return obj;
    }

}
