namespace Domain.Interfaces.Cadastros.TipoDeUnidade
{
    public interface ITipoDeUnidadeRepository
    {
        Task<IEnumerable<Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade>> GetAllAsync();
        Task<Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade> GetByIdAsync(int? id);
    }
}
