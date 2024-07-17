namespace Domain.Interfaces.Cadastros.RelatorioManutencao
{
    public interface IRelatorioManutencaoComponenteImagemRepository
    {
        Task AddAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponenteImagem obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponenteImagem obj);
        Task DeleteAsync(int id);
        Task<Entidades.Cadastros.RelatorioManutencao.RelatorioManutencaoComponenteImagem> GetByIdAsync(int id);
    }
}
