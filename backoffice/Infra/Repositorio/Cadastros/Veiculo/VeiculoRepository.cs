using Domain.Interfaces.Cadastros.Veiculo;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Veiculo;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly ContextBase _contextBase;

    public VeiculoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Veiculo.Veiculo obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if (entityToRemove is not null)
        {
            _contextBase.Veiculo.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Veiculo.Veiculo>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.Veiculo
            .AsNoTracking()
            .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
            .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Veiculo.Veiculo> GetByIdAsync(int id, int idEmpresa)
    {
        var obj = await _contextBase.Veiculo
         .FirstOrDefaultAsync(x => x.Id == id
            && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
        return obj;
    }

    public async Task UpdateKmAtualAsync(int? id, int? kmAtual)
    {
        if (id == null)
        {
            throw new ArgumentException("O ID não pode ser nulo.");
        }

        var objeto = await _contextBase.Veiculo.FindAsync(id);
        if (objeto == null)
        {
            throw new KeyNotFoundException("A bateria com o ID fornecido não foi encontrada.");
        }

        if (kmAtual.HasValue)
        {
            objeto.KM_Atual = kmAtual.Value;
        }


        _contextBase.Veiculo.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Veiculo.Veiculo obj)
    {
        var objeto = await _contextBase.Veiculo.FindAsync(obj.Id);
        objeto.Marca = obj.Marca;
        objeto.Modelo = obj.Modelo;
        objeto.Placa = obj.Placa; //checar especificamente esse aqui por ter o index de unicidade
        objeto.KM_Inicial = obj.KM_Inicial;
        objeto.KM_Atual = obj.KM_Atual;
        objeto.KM_Inspecao = obj.KM_Inspecao;
        objeto.KM_EntreRevisoes = obj.KM_EntreRevisoes;
        objeto.CapacidadeLitros = obj.CapacidadeLitros;
        objeto.QtdAtualLitros = obj.QtdAtualLitros;

        _contextBase.Veiculo.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
