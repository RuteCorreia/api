using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Tipo_Produto;

public class TipoProduto
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataSituacao { get; private set; } =
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
}
