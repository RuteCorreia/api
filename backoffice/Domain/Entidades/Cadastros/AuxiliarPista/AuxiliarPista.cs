using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.AuxiliarPista
{
    public class AuxiliarPista
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}
