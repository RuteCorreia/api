
namespace Domain.Interfaces.Cadastros.IdentificadorFrotas
{
    public interface IIdentificadorFrotasRepository
    {
        Task<int> AddAsync(int idEmpresa);
    }
}
