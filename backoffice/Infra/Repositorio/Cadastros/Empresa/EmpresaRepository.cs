using Domain.Enums;
using Domain.Interfaces.Cadastros.Empresa;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Empresa;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly ContextBase _contextBase;

    public EmpresaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.Empresa obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int id, EStatusEmpresa status)
    {
        var entityToBeChanged = await GetByIdAsync(id);
        if(entityToBeChanged is not null)
        {
            entityToBeChanged.Status = status;
            _contextBase.Empresa.Update(entityToBeChanged);
            await _contextBase.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Empresa não encontrada");
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Empresa>> GetByNameAsync(string name)
    {
        var obj = await _contextBase.Empresa.Where(w => w.Nome.Contains(name)).ToListAsync();
        return obj;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Empresa>> GetAllAsync()
    {
        var entities = await _contextBase.Empresa.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Empresa> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Empresa.FindAsync(id);
        return obj;
    }

    public async Task<string> GetLogoByIdAsync(int id)
    {
        var base64 = "";
        var obj = await _contextBase.Empresa.Where(x => x.IdEmpresa == id).Select(x => x.Imagem).FirstOrDefaultAsync();
        if(obj != null) base64 = Convert.ToBase64String(obj);

        return base64;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.Empresa obj)
    {
        var objeto = await GetByIdAsync(obj.IdEmpresa);
        objeto.Nome = obj.Nome;
        objeto.Imagem = obj.Imagem;
        objeto.Email = obj.Email;
        objeto.Telefone = obj.Telefone;
        objeto.RegistroMapa = obj.RegistroMapa;
        objeto.CNPJ = obj.CNPJ;
        objeto.InscricaoEstadual = obj.InscricaoEstadual;
        objeto.NrCDA = obj.NrCDA;
        objeto.CEP = obj.CEP;
        objeto.Endereco = obj.Endereco;
        obj.Numero = obj.Numero;
        objeto.Estado = obj.Estado;
        objeto.Cidade = obj.Cidade;
        objeto.FrotaRelatoriosAplicacaoIncendio = obj.FrotaRelatoriosAplicacaoIncendio;
        objeto.Manutencao = obj.Manutencao;
        objeto.QtdAeronaves = obj.QtdAeronaves;
        objeto.QtdDrones = obj.QtdDrones;
        objeto.QtdVeiculos = obj.QtdVeiculos;

        _contextBase.Empresa.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
