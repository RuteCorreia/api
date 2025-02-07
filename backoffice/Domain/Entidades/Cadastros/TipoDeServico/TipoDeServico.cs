using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.TipoDeServico
{
    public class TipoDeServico
    {
        [Key]
        public int Id { get; set; }
        public string? NomeServico { get; set; }
        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
