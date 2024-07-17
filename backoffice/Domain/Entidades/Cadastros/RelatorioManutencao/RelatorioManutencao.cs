using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencao
    {
        [Key]
        public int Id { get; set; }
        public bool IsMapa { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        public string? NomeRelatorio { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? StatusEnvio { get; set; }

        [ForeignKey("Aeronave")]
        public int? IdAeronave { get; set; }

        public string? Horimetro { get; set; }

        [JsonIgnore]
        public virtual Aeronave.Aeronave? Aeronave { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}