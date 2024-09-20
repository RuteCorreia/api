using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Gerador
{
    public class Gerador
    {
        [Key]
        public int Id { get; set; }
        public string NomeGerador { get; set; }
        public int QuantidadeHoras { get; set; }
        public DateTime DataUltimaTrocaOleo { get; set; }
        public int QuantidadeHorasTroca { get; set; }
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}
