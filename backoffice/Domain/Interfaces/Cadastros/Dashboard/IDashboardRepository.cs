namespace Domain.Interfaces.Cadastros.Dashboard
{
    public interface IDashboardRepository
    {
        Task<Domain.Entidades.Cadastros.Dashboard.Dashboard> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
        Task<Domain.Entidades.Cadastros.Dashboard.Dashboard> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
    }
}
