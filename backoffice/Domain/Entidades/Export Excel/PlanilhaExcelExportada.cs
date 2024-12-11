using Domain.Entidades.Cadastros.Empresa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Export_Excel
{
    public class PlanilhaExcelExportada
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Dados { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa? Empresa { get; set; }
    }
}
