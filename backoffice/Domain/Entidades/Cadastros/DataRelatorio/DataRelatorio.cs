using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.DataRelatorio
{
    public class DataRelatorio
    {
        [Key]
        public int Id { get; set; }
        public string? Data { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}
