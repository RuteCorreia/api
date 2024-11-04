
namespace Domain.Interfaces.Cadastros.IdentificadorAplicacao
{
    public interface IIdentificadorAplicacaoRepository
    {
        Task<int> AddAsync(int idEmpresa);
    }
}
