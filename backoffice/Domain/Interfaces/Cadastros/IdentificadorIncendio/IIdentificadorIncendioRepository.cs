
namespace Domain.Interfaces.Cadastros.IdentificadorIncendio
{
    public interface IIdentificadorIncendioRepository
    {
        Task<int> AddAsync(int idEmpresa);
    }
}
