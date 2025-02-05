using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Veiculante;

public class Veiculante
{
    [Key]
    public int IdVeiculante { get; set; }
    public string Nome { get; set; }
    public DateTime DataSituacao { get; private set; } =
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
}
