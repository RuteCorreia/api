namespace Domain.Interfaces.Cadastros.TipoDeFormulacao
{
    public interface ITipoDeFormulacaoRepository
    {
        Task AddAsync(Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj);
        Task UpdateAsync(Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao obj, int idEmpresa);
        Task DeleteAsync(int id, int idEmpresa);
        Task<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByNameAsync(string name, int? idEmpresa);
        Task<IEnumerable<Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetAllAsync(int idEmpresa);
        Task<Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao> GetByIdAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
    }
}
