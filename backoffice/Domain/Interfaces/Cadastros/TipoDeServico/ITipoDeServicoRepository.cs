namespace Domain.Interfaces.Cadastros.TipoDeServico
{
    public interface ITipoDeServicoRepository
    {
        Task AddAsync(Entidades.Cadastros.TipoDeServico.TipoDeServico obj);
        Task UpdateAsync(Entidades.Cadastros.TipoDeServico.TipoDeServico obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.TipoDeServico.TipoDeServico>> GetAllAsync();
        Task<Entidades.Cadastros.TipoDeServico.TipoDeServico> GetByIdAsync(int id);
    }
}
