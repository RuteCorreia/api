using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Alvo_Biologico;

public class AlvoBiologico
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Produto")]
    public int? IdProduto { get; set; }
    public string? Nome { get; set; }
    public string? DoseProdutoPorHectare { get; set; }

    [ForeignKey("Cultura")]
    public int? IdCultura { get; set; }
    [ForeignKey("TipoDeUnidade")]
    public int? IdTipoDeUnidade { get; set; }
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    public int? IdRef { get; set; }

    public int? CampoExcluido { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

    [JsonIgnore]
    public virtual Produto.Produto? Produto { get; set; }
    [JsonIgnore]
    public virtual Cultura.Cultura? Cultura { get; set; }
    [JsonIgnore]
    public virtual TipoDeUnidade? TipoDeUnidade { get; set; }
    
    public DateTime DataSituacao { get; private set; } = 
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
}
