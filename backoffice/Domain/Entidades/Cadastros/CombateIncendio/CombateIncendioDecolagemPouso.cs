using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.CombateIncendio;

public class CombateIncendioDecolagemPouso
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("CombateIncendio")]
    public int? IdCombateIncendio { get; set; }
    public DateTime? DecolagemHorario { get; set; }
    public string? DecolagemHorimetro { get; set; }
    public DateTime? PousoHorario { get; set; }
    public string? PousoHorimetro { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual CombateIncendio? CombateIncendio { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
