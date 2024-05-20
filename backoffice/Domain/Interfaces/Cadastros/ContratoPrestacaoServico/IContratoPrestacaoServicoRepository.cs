namespace Domain.Interfaces.Cadastros.ContratoPrestacaoServico;

public interface IContratoPrestacaoServicoRepository
{
    Task AddAsync(Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico obj);
    Task UpdateAsync(Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico> GetByIdAsync(int id, int idEmpresa);
}
