using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.TipoDeFormulacao
{
    public class TipoDeFormulacao
    {
        [Key]
        public int Id { get; set; }
        public string? NomeFormulacao { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        public int? IdRef { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

    }
}
