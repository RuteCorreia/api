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

    public async Task AddAsync(Domain.Entidades.Cadastros.Cliente.Cliente obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cliente.Cliente>> GetAllAsync()
    {
        var entities = await _contextBase.Cliente.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Cliente.Cliente> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Cliente.FindAsync(id);
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
}
