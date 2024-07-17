namespace Domain.Interfaces.Cadastros.RelatorioManutencao
{
    public interface IRelatorioManutencaoComponenteRepository
    {
        Task AddAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponente obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponente obj);
        Task DeleteAsync(int id);
        Task<Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponente> GetByIdAsync(int id);
    }
}
