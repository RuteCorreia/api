using Domain.Interfaces.Cadastros.ContratoPrestacaoServico;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.ContratoPrestacaoServico;

public class ContratoPrestacaoServicoRepository : IContratoPrestacaoServicoRepository
{
    private readonly ContextBase _contextBase;

    public ContratoPrestacaoServicoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
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
        var obj = await _contextBase.ContratoPrestacaoServico
           .FirstOrDefaultAsync(x => x.Id == id && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
        return obj;
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
