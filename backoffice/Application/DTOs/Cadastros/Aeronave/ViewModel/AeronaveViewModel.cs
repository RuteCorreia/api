using Application.DTOs.Cadastros.Componentes.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;
using Domain.Enums;

namespace Application.DTOs.Cadastros.Aeronave.ViewModel;

public class AeronaveViewModel
{
    public int Id { get; set; }
    public string? Fabricante { get; set; }
    public string? Prefixo { get; set; }
    public string? Modelo { get; set; }
    public string? SerialNumber { get; set; }
    public IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel>? ItensRevisao { get; set; }
    public IEnumerable<ComponentesViewModel>? Componentes { get; set; }
    public ETipoAeronave Tipo {  get; set; }
}
