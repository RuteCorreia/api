namespace Domain.Interfaces.Cadastros.Dashboard
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
        Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
        Task<IEnumerable<string>> GetUsuariosDropdownAsync(int idEmpresa);
        Task<IEnumerable<string>> GetClientesDropdownAsync(int idEmpresa);
        Task<IEnumerable<string>> GetAeronavesDropdownAsync(int idEmpresa);
    }
}
