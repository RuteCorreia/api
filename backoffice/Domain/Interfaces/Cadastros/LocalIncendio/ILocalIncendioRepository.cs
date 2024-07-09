namespace Domain.Interfaces.Cadastros.LocalIncendio
{
    public interface ILocalIncendioRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.LocalIncendio.LocalIncendio obj);
    }
}
