using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Bateria
{
    public class Bateria
    {
        [Key]
        public int Id { get; set; }
        public string NomeBateria { get; set; }
        public string NumeroBateria { get; set; }
        public int CicloAtual { get; set; }
        public int CicloMaximo { get; set; }
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
