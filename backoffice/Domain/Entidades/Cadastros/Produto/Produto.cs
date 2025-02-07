using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Produto;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Cultura")]
    public int? IdCultura { get; set; }
    public string? Nome { get; set; }
    public string? ClassificacaoToxicologica { get; set; }
    public string? Classe { get; set; }
    public string? TipoFormulacao { get; set; }
    public string? TipoServico { get; set; }
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    [ForeignKey("TipoDeFormulacao")]
    public int? IdTipoDeFormulacao { get; set; }
    [ForeignKey("TipoDeServico")]
    public int? IdTipoDeServico { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
    [JsonIgnore]
    public virtual TipoDeFormulacao.TipoDeFormulacao? TipoDeFormulacao { get; set; }
    [JsonIgnore]
    public virtual TipoDeServico.TipoDeServico? TipoDeServico { get; set; }

    [JsonIgnore]
    public virtual Cultura.Cultura? Cultura { get; set; }
    
    public DateTime DataSituacao { get; private set; } =
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
}
