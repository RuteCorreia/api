using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Domain.Interfaces.Cadastros.TelaPrincipal
{
    public interface IRelatorioAeronaveRepository
    {
        Task<IEnumerable<RelatorioAeronave>> GetAllAplicacaoAsync(DateTime? dataFiltro, int idEmpresa);
        Task<IEnumerable<RelatorioAeronave>> GetAllIncendioAsync(DateTime? dataFiltro, int idEmpresa);
        decimal? GetComissaoAsync(string nome, int idEmpresa);
    }
}
