using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Domain.Interfaces.Cadastros.TelaPrincipal
{
    public interface IRelatorioAeronaveRepository
    {
        Task<IEnumerable<RelatorioAeronave>> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa);
        Task<IEnumerable<RelatorioAeronave>> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa);
        Task<IEnumerable<RelatorioAeronave>> GetAllFrotasAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa);
        decimal? GetComissaoAsync(string nome, int idEmpresa);
    }
}
