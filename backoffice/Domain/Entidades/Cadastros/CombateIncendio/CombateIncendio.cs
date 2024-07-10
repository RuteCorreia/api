using Domain.Entidades.Cadastros.Pistas;
using Domain.Entidades.User;
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

    public string? NomeRelatorio { get; set; }

    [ForeignKey("Executor")]
    public Guid? IdExecutor { get; set; }
    public bool OrgaoPublico_Privado { get; set; }
    public string? Aviso { get; set; }

    [ForeignKey("Aeronave")]
    public int? IdAeronave { get; set; }

    [ForeignKey("Pista")]
    public int? IdPista { get; set; }
    public string? Data { get; set; }

    [ForeignKey("DataRelatorio")]
    public int? IdData { get; set; }
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
    public string? Cliente { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
    public string? Observacao { get; set; }
    public string? CapacidadeCargaAeronave { get; set; }
    public string? Piloto { get; set; }

    [ForeignKey("ContratoPrestacaoServico")]
    public int? ContratoPrestacaoServicoId { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime? DataAlteracao { get; set; }
    public int? StatusEnvio { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
    [JsonIgnore]
    public virtual Usuario? Executor { get; set; }
    [JsonIgnore]
    public virtual Aeronave.Aeronave? Aeronave { get; set; }
    [JsonIgnore]
    public virtual Pista? Pista { get; set; }
    [JsonIgnore]
    public virtual ContratoPrestacaoServico.ContratoPrestacaoServico? ContratoPrestacaoServico { get; set; }

    [JsonIgnore]
    public virtual DataRelatorio.DataRelatorio? DataRelatorio { get; set; }
}
