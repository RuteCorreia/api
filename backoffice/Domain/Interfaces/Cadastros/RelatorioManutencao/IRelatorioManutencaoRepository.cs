namespace Domain.Interfaces.Cadastros.RelatorioManutencao
{
    public interface IRelatorioManutencaoRepository
    {
        Task AddAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao obj);
        Task DeleteAsync(int id);
        Task<Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao> GetByIdAsync(int id);
    }
}
