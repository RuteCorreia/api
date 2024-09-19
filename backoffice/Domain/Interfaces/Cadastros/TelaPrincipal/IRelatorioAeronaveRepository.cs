using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Domain.Interfaces.Cadastros.TelaPrincipal
{
    public interface IRelatorioAeronaveRepository
    {
        Task<IEnumerable<RelatorioAeronave>> GetAllAplicacaoAsync();
        Task<IEnumerable<RelatorioAeronave>> GetAllIncendioAsync();
        decimal? GetComissaoAsync(string nome, int idEmpresa);
    }
}
