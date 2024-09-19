using Application.DTOs.Cadastros.TelaPrincipal.ViewModel;
using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Application.DTOs.Cadastros.TelaPrincipal.Interface
{
    public interface IRelatorioAeronaveService
    {
        Task<IEnumerable<RelatorioAeronaveDetalhadoViewModel>> GetAllAsync(DateTime? dataFiltro, string? idEmpresa);
    }
}
