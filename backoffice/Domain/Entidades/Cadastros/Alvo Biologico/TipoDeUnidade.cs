using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Alvo_Biologico
{
    public class TipoDeUnidade
    {
        [Key]
        public int Id { get; set; }
        public string? NomeUnidade { get; set; }
        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
