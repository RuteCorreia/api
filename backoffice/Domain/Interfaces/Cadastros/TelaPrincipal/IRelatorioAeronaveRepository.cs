using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Domain.Interfaces.Cadastros.TelaPrincipal
{
    public interface IRelatorioAeronaveRepository
    {
        Task<IEnumerable<RelatorioAeronave>> GetAllAsync();
    }
}
