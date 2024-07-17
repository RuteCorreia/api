namespace Domain.Interfaces.Cadastros.RelatorioManutencao
{
    public interface IRelatorioManutencaoRevisaoRepository
    {
        Task AddAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoRevisao obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoRevisao obj);
        Task DeleteAsync(int id);
        Task<Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoRevisao> GetByIdAsync(int id);
    }
}
