using Domain.Entidades.Cadastros.Pistas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.CombateIncendio;

public class CombateIncendio
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [ForeignKey("Executor")]
    public int? IdExecutor { get; set; }
    public bool OrgaoPublico_Privado { get; set; }
    public string? Aviso { get; set; }

    [ForeignKey("Aeronave")]
    public int? IdAeronave { get; set; }

    [ForeignKey("Pista")]
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

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
    [JsonIgnore]
    public virtual Executor.Executor? Executor { get; set; }
    [JsonIgnore]
    public virtual Aeronave.Aeronave? Aeronave { get; set; }
    [JsonIgnore]
    public virtual Pista? Pista { get; set; }
}
