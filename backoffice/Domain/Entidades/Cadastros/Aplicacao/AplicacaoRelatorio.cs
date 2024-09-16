using Domain.Entidades.Cadastros.Pistas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoRelatorio
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }

    [ForeignKey("Pista")]
    public int? IdPista { get; set; }
    public decimal? Dosagem { get; set; }
    public string KG_LT { get; set; }
    public int? VolumeAplicacao { get; set; }
    public decimal? TotalAreaAplicada { get; set; }
    public string? Alteracoes_Observacoes { get; set; }
    public string? Cultura { get; set; }
    public string? ProdutoAplicado { get; set; }
    public string? LocalizacaoPistaCodigoICAO { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Densidade { get; set; }
    public string? RelatorioDGPS { get; set; }
    public string? UnidadeVolumeAplicacao { get; set; }
    public string? MapaAplicacao { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
    [JsonIgnore]
    public virtual Pista? Pista { get; set; }
}
