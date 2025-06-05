namespace Domain.Interfaces.Cadastros.ReceituarioAgronomico;

public interface IReceituarioAgronomicoRepository
{
    Task<int> AddAsync(Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico obj);
    Task UpdateAsync(Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId);
    Task<Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico> GetByIdAsync(int id);
}
