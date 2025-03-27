using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Motobomba
{
    public class Motobomba
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataUltimaTrocaOleo { get; set; }
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public string? Checklist { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
