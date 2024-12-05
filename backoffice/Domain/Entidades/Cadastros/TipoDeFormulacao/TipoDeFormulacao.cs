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

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

    }
}
