namespace Domain.Interfaces.Cadastros.TipoDeFormulacao
{
    public interface ITipoDeFormulacaoRepository
    {
        Task AddAsync(Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj);
        Task UpdateAsync(Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetAllAsync();
        Task<Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByIdAsync(int id);
    }
}
