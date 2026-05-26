using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Cultura;

public class Cultura
{
    [Key]
    public int IdCultura { get; set; }
    public string? Nome { get; set; }
    public string? AlvoBiologico { get; set; }
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    public string? Status { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

    public DateTime DataSituacao { get; private set; } =
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
}
