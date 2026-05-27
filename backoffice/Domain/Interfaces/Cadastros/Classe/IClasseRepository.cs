namespace Domain.Interfaces.Cadastros.Classe
{
    public interface IClasseRepository
    {
        Task<IEnumerable<Entidades.Cadastros.Classe.Classe>> GetAllAsync();
        Task<IEnumerable<Entidades.Cadastros.Classe.Classe>> GetByTipoServicoAsync(int idTipoDeServico, int idEmpresa);
        Task<Entidades.Cadastros.Classe.Classe?> GetByIdAsync(int id);
        Task<bool> ExistsDuplicateAsync(string descricao, int idTipoDeServico, int? excludeId = null);
        Task<Entidades.Cadastros.Classe.Classe> AddAsync(Entidades.Cadastros.Classe.Classe obj);
        Task UpdateAsync(Entidades.Cadastros.Classe.Classe obj);
        Task DeleteAsync(Entidades.Cadastros.Classe.Classe obj);
        Task<bool> HasProdutoVinculadoAsync(int idClasse);
    }
}
