using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.FrotaBateria
{
    public class FrotaBateria
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Bateria")]
        public int? IdBateria { get; set; }
        [ForeignKey("ControleDeFrota")]
        public int? IdFrota { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CicloInicial { get; set; }
        public int? CicloFinal { get; set; }
        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
        public virtual Bateria.Bateria? Bateria { get; set; }
        [JsonIgnore]
        public virtual Controle_De_Frota.ControleDeFrota? ControleDeFrota { get; set; }
    }
}
