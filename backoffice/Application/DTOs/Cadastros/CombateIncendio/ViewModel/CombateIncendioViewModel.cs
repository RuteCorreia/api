using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.CombateIncendio.ViewModel;

public class CombateIncendioViewModel
{
    public int Id { get; set; }
    public int? IdEmpresa { get; set; }
    public int? IdExecutor { get; set; }
    public bool OrgaoPublico_Privado { get; set; }
    public string? Aviso { get; set; }
    public int? IdAeronave { get; set; }
    public int? IdPista { get; set; }
    public DateTime? Data { get; set; }
    public DateTime? HoraInicial { get; set; }
    public string? HorimetroAviao { get; set; }
    public string? LocalIncendioLat { get; set; }
    public string? LocalIncendioLon { get; set; }
    public string? Referencia { get; set; }
    public DateTime? HorarioFinalOperacao { get; set; }
    public string? HorimetroFinalOperacao { get; set; }
    public int? TotalAguaUtilizadaOperacao { get; set; }
    public string? CoordenadorBaseOperacionalNome { get; set; }
    public string? CoordenadorBaseOperacionalPosto { get; set; }
    public string? CoordenadorBaseOperacionalRE { get; set; }
    public string? CoordenadorBaseOperacionalAssinatura { get; set; }
    public string? ComandanteOcorrenciaNome { get; set; }
    public string? ComandanteOcorrenciaPosto { get; set; }
    public string? ComandanteOcorrenciaRE { get; set; }
    public string? ComandanteOcorrenciaAssinatura { get; set; }
    public string? ResponsavelOcorrenciaNome { get; set; }
    public string? ResponsavelOcorrenciaPosto { get; set; }
    public string? ResponsavelOcorrenciaRE { get; set; }
    public string? ResponsavelOcorrenciaAssinatura { get; set; }
}
