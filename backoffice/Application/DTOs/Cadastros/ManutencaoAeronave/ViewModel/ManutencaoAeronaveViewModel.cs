using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;

public class ManutencaoAeronaveViewModel
{
    public int Id { get; set; }
    public int? IdAeronave { get; set; }
    public string? Horimetro { get; set; }
    public string? DocumentoBase64 { get; set; }
    public string? FichaInspecaoBase64 { get; set; }
    public string? ManualAeronaveBase64 { get; set; }
    public string? MapaComponentesBase64 { get; set; }
    public string? PrefixoAeronave { get; set; }
    public byte[]? Documento { get; set; }
    public byte[]? FichaInspecao { get; set; }
    public byte[]? ManualAeronave { get; set; }
    public byte[]? MapaComponentes { get; set; }
    public IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel> ItensRevisao { get; set; }
}
