using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.FrotaMotobomba
{
    public class FrotaMotobomba
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ControleDeFrota")]
        public int? IdFrota { get; set; }
        [ForeignKey("Motobomba")]
        public int? IdMotobomba { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Identificacao { get; set; }
        public double? LitrosOleo { get; set; }
        public double? LitrosGasolina { get; set; }
        public string? CheckList { get; set; }
        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public virtual Motobomba.Motobomba? Motobomba { get; set; }
        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
        [JsonIgnore]
        public virtual Controle_De_Frota.ControleDeFrota? ControleDeFrota { get; set; }
    }
}
