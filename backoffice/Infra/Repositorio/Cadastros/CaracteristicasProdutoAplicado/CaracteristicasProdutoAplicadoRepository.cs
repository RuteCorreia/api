using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.CaracteristicasProdutoAplicado;

public class CaracteristicasProdutoAplicadoRepository : ICaracteristicasProdutoAplicadoRepository
{
    private readonly ContextBase _contextBase;

    public CaracteristicasProdutoAplicadoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if(entityToRemove is not null)
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.CaracteristicasProdutoAplicado
            .AsNoTracking()
            .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
            .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetByIdAsync(int id, int idEmpresa)
    {
        var obj = await _contextBase.CaracteristicasProdutoAplicado
            .FirstOrDefaultAsync(x => x.Id == id && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj)
    {
        var objeto = await _contextBase.CaracteristicasProdutoAplicado.FindAsync(obj.Id);
        objeto.Cultura = obj.Cultura;
        objeto.ReceiturarioAgronomico = obj.ReceiturarioAgronomico;
        objeto.NomeProduto = obj.NomeProduto;
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.Classe = obj.Classe;
        objeto.TipoFormulacao = obj.TipoFormulacao;
        objeto.AlvoBiologico = obj.AlvoBiologico;
        objeto.DoseProdutoHectare = obj.DoseProdutoHectare;
        objeto.UnidadeDoseProdutoHectare = obj.UnidadeDoseProdutoHectare;
        objeto.Adjuvante = obj.Adjuvante;
        objeto.TipoServico = obj.TipoServico;
        objeto.NumeroReceituarioAgronomico = obj.NumeroReceituarioAgronomico;
        objeto.DataEmissao = obj.DataEmissao;

        _contextBase.CaracteristicasProdutoAplicado.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
